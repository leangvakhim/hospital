using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital
{
    public partial class FormAppointment : Form
    {
        private string appointment_username;
        private string appointment_role;
        MySqlConnection conn;
        String MySQLConn = "";
        MySqlCommand command;
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        String sqlquery = "SELECT * FROM tbappointment WHERE active = 1 ORDER BY id DESC";
        public FormAppointment(string appointment_username, string appointment_role)
        {
            InitializeComponent();
            this.appointment_username = appointment_username;
            this.appointment_role = appointment_role;
            this.Text = "Appointment day Management";
            MySQLConn = "server=127.0.0.1; user=root; database=hospital; password=";
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                //MessageBox.Show("Connection success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }
        private void FormAppointment_Load(object sender, EventArgs e)
        {
            Refresh();
            DisplayInComboBox();
        }

        private void Refresh()
        {
            txtID.Enabled = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            cbspecilization.SelectedIndex = 0;
            buttonSave = false;
            buttonEdit = false;
            buttonRemove = false;
            buttonReport = false;
            buttonSearch = false;
            if (appointment_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (appointment_role == "Create Only")
            {
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(sqlquery, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[4].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbappointment ORDER BY id DESC LIMIT 1", conn);
                object result = command_id.ExecuteScalar();
                int maxId = 0;
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            DateTime DOB;
            if (btnSave.Text == "Save")
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Please enter ID.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (cbspecilization.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Specialization of the docctor.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    btnEdit.Enabled = false;
                    buttonSave = true;
                    // check duplicated appointment date & time
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        DateTime cellDate;
                        if (DateTime.TryParse(row.Cells[3].Value.ToString(), out cellDate))
                        {
                            if (cellDate.Date == BookDate.Value.Date)
                            {
                                MessageBox.Show("This time is not available. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                conn.Close();
                                return;
                            }
                        }
                    }
                    conn.Open();
                    string id = txtID.Text;
                    string name = txtName.Text;
                    string specialization = cbspecilization.Text;
                    DOB = BookDate.Value;

                    string query = "INSERT INTO tbappointment(id, name, doctorspecialization, bookdate) VALUES (@id, @name, @doctorspecialization, @bookdate)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("id", "");
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("doctorspecialization", specialization);
                    command.Parameters.AddWithValue("Bookdate", DOB);
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int currenId = int.Parse(txtID.Text);
                    int nextID = currenId + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();

                    cbspecilization.SelectedIndex = 0;
                    BookDate.Value = DateTime.Now;

                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                btnSave.Text = "Save";
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                txtID.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbappointment ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();

                cbspecilization.SelectedIndex = 0;
                BookDate.Value = DateTime.Now;
            }
        }

        private void DisplayInComboBox()
        {
            try
            {
                using (conn = new MySqlConnection(MySQLConn))
                {
                    conn.Open();
                    string query = "SELECT specialization FROM tbdoctor WHERE active = 1 ORDER BY id DESC";
                    command = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbspecilization.Items.Add(reader["specialization"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String updateQuery = "UPDATE tbappointment SET active = @newValue WHERE id = @id";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);

                command.ExecuteNonQuery();
                buttonRemove = true;
                TrackUserAction("Remove");
                txtID.Clear();
                txtName.Clear();
                cbspecilization.SelectedIndex = 0;
                BookDate.Value = DateTime.Now;
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(appointment_username, appointment_role);
            formManagement.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                btnSave.Text = "New";

                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(cbspecilization.Text) && row.Cells[3].Value.ToString().Equals(BookDate))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                string name = txtName.Text;
                string specilization = cbspecilization.SelectedItem.ToString();
                DateTime bookdate = BookDate.Value;
                String updateQuery = "UPDATE tbappointment SET name = @newName, doctorspecialization = @newSpecialization, bookdate = @newbookdate WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("id", txtID.Text);
                update_command.Parameters.AddWithValue("newName", name);
                update_command.Parameters.AddWithValue("newSpecialization", specilization);
                update_command.Parameters.AddWithValue("newbookdate", bookdate);

                update_command.ExecuteNonQuery();
                buttonEdit = true;
                TrackUserAction("Edit");

                txtName.Clear();
                cbspecilization.SelectedIndex = 0;
                BookDate.Value = DateTime.Now;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM tbappointment WHERE name LIKE @name && active = 1 ORDER BY id DESC";
            buttonSearch = true;
            if (txtName.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (MySqlConnection conn = new MySqlConnection(MySQLConn))
            {
                using (MySqlCommand command = new MySqlCommand(searchQuery, conn))
                {
                    command.Parameters.AddWithValue("@name", "%" + txtName.Text + "%");

                    try
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = table;
                        dataGridView1.RowTemplate.Height = 30;
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.ReadOnly = true;
                        dataGridView1.Columns[4].Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            btnSave.Text = "New";
            TrackUserAction("Search");
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ContainsSpecialCharacters(txtName.Text))
            {
                txtName.BorderStyle = BorderStyle.FixedSingle;
                txtName.BackColor = System.Drawing.Color.White;
                txtName.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtName.BorderStyle = BorderStyle.FixedSingle;
                txtName.BackColor = System.Drawing.SystemColors.Window;
                txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void FormAppointment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(appointment_username, appointment_role, FormReport._ReportType.Appointment, sqlquery);
            report.Show();
            buttonReport = true;
            this.Hide();
            TrackUserAction("Report");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnSave.Text = "New";
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                if (appointment_role == "View Only")
                {

                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                    btnReport.Enabled = false;
                }
                else if (appointment_role == "Create Only")
                {
                    btnRemove.Enabled = false;
                    btnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string selectedValue = selectedRow.Cells[2].Value.ToString();
                int ind = cbspecilization.FindStringExact(selectedValue);
                cbspecilization.SelectedIndex = ind;
                BookDate.Value = (DateTime)selectedRow.Cells[3].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackUserAction(string userAction)
        {
            try
            {
                using (conn = new MySqlConnection(MySQLConn))
                {
                    conn.Open();
                    string query = "INSERT INTO tbrecord(userID, userName, userRole, userAction, userForm, personID, personName, actionDateTime) VALUES (@uID, @uName, @uRole, @uAction, @uForm, @pID, @pName, @aDateTime)";

                    command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("uAction", userAction);
                    command.Parameters.AddWithValue("uForm", "Appointment");
                    command.Parameters.AddWithValue("uID", "");
                    command.Parameters.AddWithValue("uName", appointment_username);
                    command.Parameters.AddWithValue("uRole", appointment_role);
                    if (userAction.Equals("Report"))
                    {
                        command.Parameters.AddWithValue("pID", "");
                        command.Parameters.AddWithValue("pName", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("pID", txtID.Text);
                        command.Parameters.AddWithValue("pName", txtName.Text);
                    }
                    command.Parameters.AddWithValue("aDateTime", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return text.Any(c => !allowedCharacters.Contains(c));
        }
    }
}

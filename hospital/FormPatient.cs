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
    public partial class FormPatient : Form
    {
        private string patient_username;
        private string patient_role;
        MySqlConnection conn;
        MySqlCommand command;
        String MySQLConn = "";
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        private string sqlquery = "SELECT * FROM tbpatient WHERE active = 1 ORDER BY id DESC";
        public FormPatient(string patient_username, string patient_role)
        {
            InitializeComponent();
            this.patient_username = patient_username;
            this.patient_role = patient_role;
            this.Text = "Patient Management";
            MySQLConn = "server=127.0.0.1; user=root; database=hospital; password=";
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                //MessageBox.Show("Connection success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            finally { conn.Close(); }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(patient_username, patient_role);
            formManagement.Show();
            this.Hide();
        }

        private void FormPatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                if (txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                buttonEdit = true;
                btnSave.Text = "New";
                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[3].Value.ToString().Equals(txtaddress.Text))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                string gender = "";
                if (buttonMale.Checked)
                {
                    gender = "Male";
                }
                else if (buttonFemale.Checked)
                {
                    gender = "Female";
                }

                String updateQuery = "UPDATE tbpatient SET name = @newName, gender = @newGender, address = @newAddress WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newGender", gender);
                update_command.Parameters.AddWithValue("newAddress", txtaddress.Text);
                update_command.Parameters.AddWithValue("id", txtID.Text);

                update_command.ExecuteNonQuery();
                TrackUserAction("Edit");

                txtName.Clear();
                txtaddress.Clear();
                buttonMale.Checked = false;
                buttonFemale.Checked = false;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter patient's name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return;
                }else if(txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtaddress.Text == "")
                {
                    MessageBox.Show("Please enter patient's address.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtaddress.Focus();
                    return;
                }
                try
                {
                    btnEdit.Enabled = false;
                    buttonSave = true;
                    // check duplicated data
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(txtName.Text))
                        {
                            MessageBox.Show("Duplicate Patient's Name. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string name = txtName.Text;
                    string gender = "";
                    if (buttonMale.Checked)
                    {
                        gender = "Male";
                    }
                    else if (buttonFemale.Checked)
                    {
                        gender = "Female";
                    }
                    string address = txtaddress.Text;

                    string query = "INSERT INTO tbpatient(id, name, gender, address) VALUES (@id, @name, @gender, @address)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@address", address);
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    txtaddress.Clear();
                    buttonMale.Checked = false;
                    buttonFemale.Checked = false;

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
                txtID.Enabled = false;
                btnRemove.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbpatient ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                txtaddress.Clear();
                buttonMale.Checked = false;
                buttonFemale.Checked = false;
            }
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            btnRemove.Enabled = false;
            if (patient_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (patient_role == "Create Only")
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
                dataGridView1.RowTemplate.Height = 60;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[4].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbpatient ORDER BY id DESC LIMIT 1", conn);
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

        private void FormPatient_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtName.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnSave.Text = "New";
            buttonSearch = true;
            btnEdit.Enabled = true;
            btnRemove.Enabled = true;
            if (patient_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (patient_role == "Create Only")
            {
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbpatient WHERE @name = name && active = 1", conn);
                command.Parameters.AddWithValue("name", txtName.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count < 0)
                {
                    MessageBox.Show("No data Found!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtID.Text = table.Rows[0][0].ToString();
                    txtName.Text = table.Rows[0][1].ToString();
                    if (table.Rows[0][2].Equals("Male"))
                    {
                        buttonMale.Checked = true;
                    }
                    else
                    {
                        buttonFemale.Checked = true;
                    }
                    txtaddress.Text = table.Rows[0][3].ToString();
                    TrackUserAction("Search");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnEdit.Enabled = false;
                btnSave.Text = "Save";
                txtID.Clear();
                txtName.Clear();
                txtaddress.Clear();
                buttonMale.Checked = false;
                buttonFemale.Checked = false;
                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                buttonRemove = true;
                String updateQuery = "UPDATE tbpatient SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();
                TrackUserAction("Remove");

                txtID.Clear();
                txtName.Clear();
                txtaddress.Clear();
                buttonMale.Checked = false;
                buttonFemale.Checked = false;
                btnSave.Text = "Save";

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnSave.Text = "New";
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                if (patient_role == "View Only")
                {
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                    btnReport.Enabled = false;
                }
                else if (patient_role == "Create Only")
                {
                    btnRemove.Enabled = false;
                    btnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                if (selectedRow.Cells[2].Value.Equals("Male"))
                {
                    buttonMale.Checked = true;
                }
                else
                {
                    buttonFemale.Checked = true;
                }
                txtaddress.Text = selectedRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            buttonReport = true;
            FormReport report = new FormReport(patient_username, patient_role, FormReport._ReportType.Patient, sqlquery);
            report.Show();
            this.Hide();
            TrackUserAction("Report");
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
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
                    command.Parameters.AddWithValue("uForm", "Patient");
                    command.Parameters.AddWithValue("uID", "");
                    command.Parameters.AddWithValue("uName", patient_username);
                    command.Parameters.AddWithValue("uRole", patient_role);
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
    }
}

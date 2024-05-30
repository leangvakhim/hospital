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
    public partial class FormBed : Form
    {
        private string bed_username;
        private string bed_role;
        private string sqlquery = "SELECT * FROM tbbed WHERE active = 1 ORDER BY id DESC";
        MySqlConnection conn;
        MySqlCommand command;
        String MySQLConn = "";
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        public FormBed(string bed_username, string bed_role)
        {
            InitializeComponent();
            this.bed_username = bed_username;
            this.bed_role = bed_role;
            this.Text = "Patient's bed Management";
            MySQLConn = "server=127.0.0.1; user=root; database=hospital; password=";
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                //MessageBox.Show("Connection success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(bed_username, bed_role);
            formManagement.Show();
            this.Hide();
        }

        private void FormBed_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void FormBed_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            btnRemove.Enabled = false;
            dateTimeCheckOut.Enabled = false;
            if (bed_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (bed_role == "Create Only")
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

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbbed ORDER BY id DESC LIMIT 1", conn);
                object result = command_id.ExecuteScalar();
                int maxId = 0;
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }else if(txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                buttonSave = true;
                try
                {
                    btnEdit.Enabled = false;
                    dateTimeCheckOut.Enabled = false;
                    // check duplicated data
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(txtName.Text))
                        {
                            MessageBox.Show("Duplicate Name. Please try again!!!");
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string name = txtName.Text;
                    DateTime CheckIn = dateTimeCheckIn.Value;
                    DateTime CheckInDate = CheckIn.Date;

                    string query = "INSERT INTO tbbed(id, name, checkIn, checkOut) VALUES (@id, @name, @checkIn, @checkOut)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@checkIn", CheckInDate);
                    command.Parameters.AddWithValue("@checkOut", "0001-01-01");
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    dateTimeCheckIn.Value = DateTime.Now;
                    dateTimeCheckOut.Value = DateTime.Now;

                    Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
                dateTimeCheckOut.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand(sqlquery, conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                dateTimeCheckIn.ResetText();
                dateTimeCheckOut.ResetText();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtName.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonSearch = true;
            btnSave.Text = "New";
            btnEdit.Enabled = true;
            btnRemove.Enabled = true;
            dateTimeCheckOut.Enabled = true;
            if (bed_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
                dateTimeCheckOut.Enabled=false;
            }
            else if (bed_role == "Create Only")
            {
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbbed WHERE @name = name && active = 1", conn);
                command.Parameters.AddWithValue("name", txtName.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count < 0)
                {
                    MessageBox.Show("No data Found!");
                }
                else
                {
                    txtID.Text = table.Rows[0][0].ToString();
                    txtName.Text = table.Rows[0][1].ToString();
                    dateTimeCheckIn.Value = (DateTime)table.Rows[0][2];
                    DateTime checkout = (DateTime)table.Rows[0][3];
                    if (checkout.ToString().Equals("1/1/0001 12:00:00 AM"))
                        dateTimeCheckOut.Value = DateTime.Now;
                    else
                        dateTimeCheckOut.Value = (DateTime)table.Rows[0][3];
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
                dateTimeCheckIn.Value = DateTime.Today;
                dateTimeCheckOut.Value = DateTime.Today;
                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.");
            }
            finally { conn.Close(); }
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
                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(dateTimeCheckIn.Value) && row.Cells[3].Value.ToString().Equals(dateTimeCheckOut.Value))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                buttonEdit = true;
                btnSave.Text = "Save";
                conn.Open();
                DateTime CheckIn = dateTimeCheckIn.Value;
                DateTime CheckInDate = CheckIn.Date;
                DateTime CheckOut = dateTimeCheckOut.Value;
                DateTime CheckOutDate = CheckOut.Date;
                String updateQuery = "UPDATE tbbed SET name = @newName, checkIn = @newCheckIn, checkOut = @newCheckOut WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newCheckIn", CheckInDate);
                update_command.Parameters.AddWithValue("id", txtID.Text);
                if(CheckInDate == CheckOutDate)
                    update_command.Parameters.AddWithValue("newCheckOut", "0001-01-01");
                else
                    update_command.Parameters.AddWithValue("newCheckOut", CheckOutDate);
                update_command.ExecuteNonQuery();
                TrackUserAction("Edit");

                txtName.Clear();
                dateTimeCheckIn.Value = DateTime.Now;
                dateTimeCheckOut.Value = DateTime.Now;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                buttonRemove = true;
                conn.Open();
                String updateQuery = "UPDATE tbbed SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();
                TrackUserAction("Remove");

                txtID.Clear();
                txtName.Clear();
                dateTimeCheckIn.Value = DateTime.Today;
                dateTimeCheckOut.Value = DateTime.Today;

                btnSave.Text = "Save";

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                dateTimeCheckOut.Enabled = true;
                if (bed_role == "View Only")
                {
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                    btnReport.Enabled = false;
                    dateTimeCheckOut.Enabled = false;
                }
                else if (bed_role == "Create Only")
                {
                    btnRemove.Enabled = false;
                    btnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                dateTimeCheckIn.Value = (DateTime)selectedRow.Cells[2].Value;
                DateTime checkout = (DateTime)selectedRow.Cells[3].Value;
                if (checkout.ToString().Equals("1/1/0001 12:00:00 AM"))
                    dateTimeCheckOut.Value = DateTime.Now;
                else
                    dateTimeCheckOut.Value = (DateTime)selectedRow.Cells[3].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(bed_username, bed_role, FormReport._ReportType.Bed, sqlquery);
            buttonReport = true;
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
                    command.Parameters.AddWithValue("uForm", "Bed");
                    command.Parameters.AddWithValue("uID", "");
                    command.Parameters.AddWithValue("uName", bed_username);
                    command.Parameters.AddWithValue("uRole", bed_role);
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

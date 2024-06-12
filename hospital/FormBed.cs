using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace hospital
{
    public partial class FormBed : Form
    {
        private string bed_username;
        private string bed_role;
        string previousBedID = "";
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
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DisplayInComboBox();
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            cbBedID.SelectedIndex = 0;
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
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[6].Visible = false;

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
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                else if (cbBedID.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Bed ID.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(cbBedID.Text))
                        {
                            MessageBox.Show("Duplicate Name. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string name = txtName.Text;
                    string bedID = cbBedID.SelectedItem.ToString();
                    DateTime CheckIn = dateTimeCheckIn.Value;
                    DateTime CheckInDate = CheckIn.Date;
                    int duration = 0;

                    string query = "INSERT INTO tbbed(id, name, bedID, checkIn, checkOut, duration) VALUES (@id, @name, @bedID, @checkIn, @checkOut, @duration)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@bedID", bedID);
                    command.Parameters.AddWithValue("@checkIn", CheckInDate);
                    command.Parameters.AddWithValue("@checkOut", "0001-01-01");
                    command.Parameters.AddWithValue("@duration", duration);
                    command.ExecuteNonQuery();

                    String removeAvailable = "UPDATE tbamountbed SET available = @newValue WHERE name = @name";
                    MySqlCommand commandAvailable = new MySqlCommand(removeAvailable, conn);

                    commandAvailable.Parameters.AddWithValue("@newValue", 0);
                    commandAvailable.Parameters.AddWithValue("@name", cbBedID.SelectedItem.ToString());
                    commandAvailable.ExecuteNonQuery();

                    TrackUserAction("Save");

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    cbBedID.Items.Remove(cbBedID.SelectedItem);
                    cbBedID.SelectedIndex = 0;
                    dateTimeCheckIn.Value = DateTime.Now;
                    dateTimeCheckOut.Value = DateTime.Now;

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
                dateTimeCheckOut.Enabled = false;
                cbBedID.SelectedIndex = 0;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand(sqlquery, conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                if (!string.IsNullOrEmpty(previousBedID))
                {
                    cbBedID.Items.Remove(previousBedID);
                }
                dateTimeCheckIn.ResetText();
                dateTimeCheckOut.ResetText();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = "SELECT * FROM tbbed WHERE name LIKE @name && active = 1 ORDER BY id DESC";
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
                        dataGridView1.Columns[6].Visible = false;
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
                btnSave.Text = "Save";
                conn.Open();
                string bedID = cbBedID.SelectedItem.ToString();
                DateTime CheckIn = dateTimeCheckIn.Value;
                DateTime CheckInDate = CheckIn.Date;
                DateTime CheckOut = dateTimeCheckOut.Value;
                DateTime CheckOutDate = CheckOut.Date;

                TimeSpan duration = CheckOutDate - CheckInDate;
                int durationInDays = (int)duration.TotalDays;

                String updateQuery = "UPDATE tbbed SET name = @newName, bedID = @newBedID, checkIn = @newCheckIn, checkOut = @newCheckOut, duration = @newDuration WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newBedID", bedID);
                update_command.Parameters.AddWithValue("newCheckIn", CheckInDate);
                update_command.Parameters.AddWithValue("id", txtID.Text);
                if (CheckInDate == CheckOutDate)
                    update_command.Parameters.AddWithValue("newCheckOut", "0001-01-01");
                else
                    update_command.Parameters.AddWithValue("newCheckOut", CheckOutDate);

                update_command.Parameters.AddWithValue("newDuration", durationInDays);
                update_command.ExecuteNonQuery();

                String removeAvailable = "UPDATE tbamountbed SET available = @newValue WHERE name = @name";
                MySqlCommand commandAvailable = new MySqlCommand(removeAvailable, conn);

                commandAvailable.Parameters.AddWithValue("@newValue", 0);
                commandAvailable.Parameters.AddWithValue("@name", cbBedID.SelectedItem.ToString());
                commandAvailable.ExecuteNonQuery();

                String restoreAvailable = "UPDATE tbamountbed SET available = @newValue WHERE name = @name";
                MySqlCommand restorecommandAvailable = new MySqlCommand(restoreAvailable, conn);

                restorecommandAvailable.Parameters.AddWithValue("@newValue", 1);
                restorecommandAvailable.Parameters.AddWithValue("@name", previousBedID);
                restorecommandAvailable.ExecuteNonQuery();

                TrackUserAction("Edit");

                txtName.Clear();
                cbBedID.Items.Remove(cbBedID.SelectedItem);
                if (string.IsNullOrEmpty(previousBedID))
                {
                    cbBedID.Items.Add(previousBedID);
                }
                cbBedID.SelectedIndex = 0;
                dateTimeCheckIn.Value = DateTime.Now;
                dateTimeCheckOut.Value = DateTime.Now;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                String removeAvailable = "UPDATE tbamountbed SET available = @newValue WHERE name = @name";
                MySqlCommand commandAvailable = new MySqlCommand(removeAvailable, conn);

                commandAvailable.Parameters.AddWithValue("@newValue", 1);
                commandAvailable.Parameters.AddWithValue("@name", cbBedID.SelectedItem.ToString());
                commandAvailable.ExecuteNonQuery();

                TrackUserAction("Remove");

                object selectedItem = cbBedID.SelectedItem;

                if (!cbBedID.Items.Contains(selectedItem))
                {
                    cbBedID.Items.Add(selectedItem);
                }

                txtID.Clear();
                txtName.Clear();
                cbBedID.SelectedIndex = 0;
                dateTimeCheckIn.Value = DateTime.Today;
                dateTimeCheckOut.Value = DateTime.Today;

                btnSave.Text = "Save";

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void DisplayInComboBox()
        {
            try
            {
                using (conn = new MySqlConnection(MySQLConn))
                {
                    conn.Open();
                    string query = "SELECT name FROM tbamountbed WHERE active = 1 && available = 1 ORDER BY id DESC";
                    command = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbBedID.Items.Add(reader["name"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                btnSave.Text = "New";
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                dateTimeCheckOut.Enabled = true;
                conn.Open();
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

                string sqlquery = "SELECT bedID FROM tbbed WHERE id = @id ORDER BY id DESC";
                MySqlCommand commandBedID = new MySqlCommand(sqlquery, conn);
                commandBedID.Parameters.AddWithValue("@id", selectedRow.Cells[0].Value.ToString());
                object result = commandBedID.ExecuteScalar();

                //cbBedID.Items.Clear();
                //DisplayInComboBox();

                if (result != null && result != DBNull.Value)
                {
                    string BedID = result.ToString();
                    cbBedID.Items.Add(BedID);
                    cbBedID.SelectedItem = BedID;

                    if (!string.IsNullOrEmpty(previousBedID))
                    {
                        if (cbBedID.Items.Contains(previousBedID))
                        {
                            cbBedID.Items.Remove(previousBedID);
                        }

                    }
                        //if (cbBedID.Items.Contains(previousBedID))
                        //cbBedID.Items.Remove(previousBedID);

                    previousBedID = BedID;
                }

                dateTimeCheckIn.Value = (DateTime)selectedRow.Cells[3].Value;
                DateTime checkout = (DateTime)selectedRow.Cells[4].Value;
                if (checkout.ToString().Equals("1/1/0001 12:00:00 AM"))
                    dateTimeCheckOut.Value = DateTime.Now;
                else
                    dateTimeCheckOut.Value = (DateTime)selectedRow.Cells[4].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conn.Close(); }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string reportQuery = "SELECT * FROM tbbed WHERE name LIKE '%"+txtName.Text+"%' AND active = 1 ORDER BY id DESC";
            FormReport report = new FormReport(bed_username, bed_role, FormReport._ReportType.Bed, reportQuery);
            buttonReport = true;
            report.Show();
            this.Hide();
            TrackUserAction("Report");
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

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
            catch (Exception ex) { MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

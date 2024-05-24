using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace hospital
{
    public partial class FormAppointment : Form
    {
        private string appointment_username;
        private string appointment_role;
        MySqlConnection conn;
        MySqlCommand command;
        //MySqlConnection conn;
        String MySQLConn = "";
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        private string sqlquery = "SELECT * FROM tbappointment WHERE active = 1 ORDER BY id DESC";
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
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
        private void FormAppointment_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            //btnEdit.Enabled = false;
            
            //btnRemove.Enabled = false;
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
            buttonSearch = true;
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
                TrackUserAction("Search");

                /*DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn)dataGridView1.Columns[3];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;*/

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
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            DateTime DOB;
            if (btnSave.Text == "Save")
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Please enter ID.");
                    return;
                }
                else if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.");
                    return;
                }
                else if (txtspecialization.Text == "")
                {
                    MessageBox.Show("Please enter specialization.");
                    return;
                }
                else if (BookDate.Text == "")
                {
                    MessageBox.Show("Please input Book date.");
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
                            MessageBox.Show("Duplicate Name. Please try again!!!");
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string id = txtID.Text;
                    string name = txtName.Text;
                    string specialization = txtspecialization.Text;
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

                    txtspecialization.SelectedIndex = -1;
                    BookDate.Value =DateTime.Now;

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
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbappointment ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                
                txtspecialization.SelectedIndex = -1;
                BookDate.Value = DateTime.Now;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(appointment_username, appointment_role, FormReport._ReportType.Appointment, sqlquery);
            report.Show();
            buttonReport = true;
            this.Hide();
            TrackUserAction("Report");
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
                    command.Parameters.AddWithValue("uForm", "Ambulance");
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {

                conn.Open();
                String updateQuery = "UPDATE tbappointment SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();

                txtID.Clear();
                txtName.Clear();
                txtspecialization.SelectedIndex = -1;
                BookDate.Value = DateTime.Now;
                TrackUserAction("Remove");
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(appointment_username, appointment_role);
            formManagement.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
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
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[3].Value.ToString().Equals(txtspecialization.Text) && row.Cells[4].Value.ToString().Equals(BookDate))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                String updateQuery = "UPDATE tbappointment SET name = @newName, phone = @newPhone, doctorspecialization = @newSpecialization, bookdate = @newbookdate WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("id", txtID.Text);
                update_command.Parameters.AddWithValue("newName", txtName.Text);                
                update_command.Parameters.AddWithValue("newSpecialization", txtspecialization.Text);
                update_command.Parameters.AddWithValue("newbookdate", BookDate);
               

                update_command.ExecuteNonQuery();

                txtName.Clear();
                txtspecialization.SelectedIndex = -1;
                BookDate.Value = DateTime.Now;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            btnSave.Text = "New";
            btnEdit.Enabled = true;
            if (appointment_role == "View Only")
            {
                
                btnRemove.Enabled = false;
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (appointment_role == "Create Only")
            {
                
                btnEdit.Enabled = false;
                btnSave.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbappointment WHERE @name = name && active = 1", conn);
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
                    txtspecialization.Text = table.Rows[0][3].ToString();
                    BookDate.Text = table.Rows[0][3].ToString();

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnEdit.Enabled = false;
                btnSave.Text = "Save";
                txtID.Clear();
                txtName.Clear();
                txtspecialization.SelectedIndex = -1;
                BookDate.Value = DateTime.Now;

                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.");
            }
            finally { conn.Close(); }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            e.Equals(txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }

        private void bookdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtspecialization.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            BookDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}

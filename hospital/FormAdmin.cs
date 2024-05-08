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
    public partial class FormAdmin : Form
    {
        private string admin_username;
        private string admin_role;
        String MySQLConn = "";
        MySqlConnection conn;
        public FormAdmin(string admin_username, string admin_role)
        {
            InitializeComponent();
            this.Text = "Admin";
            this.admin_username = admin_username;
            this.admin_role = admin_role;
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

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(admin_username, admin_role);
            formManagement.Show();
            this.Hide();
        }

        private void FormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.");
                    txtName.Focus();
                    return;
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter password.");
                    txtPassword.Focus();
                    return;
                }
                else if (txtConfirmPassword.Text == "")
                {
                    MessageBox.Show("Please enter confirm password.");
                    txtConfirmPassword.Focus();
                    return;
                }
                else if (cbPosition.SelectedIndex != 1 && cbPosition.SelectedIndex != 2 && cbPosition.SelectedIndex != 3)
                {
                    MessageBox.Show("Please set user permission.");
                    cbPosition.Focus();
                    return;
                }
                try
                {
                    btnEdit.Enabled = false;
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
                    string password = txtPassword.Text;
                    string confirmPassword = txtConfirmPassword.Text;
                    string position = "";
                    if (cbPosition.SelectedIndex == 1)
                    {
                        position = "View Only";
                    }
                    else if (cbPosition.SelectedIndex == 2)
                    {
                        position = "Create Only";
                    }
                    else if (cbPosition.SelectedIndex == 3)
                    {
                        position = "Super Admin";
                    }

                    if (!password.Equals(confirmPassword))
                    {
                        MessageBox.Show("Incorrect Confirmation Password.");
                        txtPassword.Focus();
                        return;
                    }

                    string query = "INSERT INTO tbadmin(id, name, position, password) VALUES (@id, @name, @position, @password)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("id", "");
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("position", position);
                    command.Parameters.AddWithValue("password", password);
                    command.ExecuteNonQuery();

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    txtPassword.Clear();
                    txtConfirmPassword.Clear();
                    cbPosition.SelectedIndex = 0;

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
                txtID.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbadmin ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                cbPosition.SelectedIndex = 0;
                txtConfirmPassword.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbadmin WHERE active = 1 ORDER BY id DESC", conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.RowTemplate.Height = 60;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbadmin ORDER BY id DESC LIMIT 1", conn);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSave.Text = "New";
            btnEdit.Enabled = true;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String position;
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbadmin WHERE @name = name && active = 1", conn);
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
                    position = table.Rows[0][2].ToString();
                    if (position.Equals("View Only"))
                    {
                        cbPosition.SelectedIndex = 1;
                    }
                    else if (position.Equals("Create Only"))
                    {
                        cbPosition.SelectedIndex = 2;
                    }
                    else if (position.Equals("Super Admin"))
                    {
                        cbPosition.SelectedIndex = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnEdit.Enabled = false;
                btnSave.Text = "New";
                txtID.Clear();
                txtName.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                cbPosition.SelectedIndex = 0;
                Refresh();
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                btnSave.Text = "Save";
                MessageBox.Show("Name not found in the list. Please try again.");
            }
            finally { conn.Close(); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                btnSave.Text = "New";
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(cbPosition.SelectedText))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                string position = "";
                if (cbPosition.SelectedIndex == 1)
                {
                    position = "View Only";
                }
                else if (cbPosition.SelectedIndex == 2)
                {
                    position = "Create Only";
                }
                else if (cbPosition.SelectedIndex == 3)
                {
                    position = "Super Admin";
                }
                String updateQuery = "UPDATE tbadmin SET name = @newName, position = @newPosition WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newPosition", position);
                update_command.Parameters.AddWithValue("id", txtID.Text);

                update_command.ExecuteNonQuery();

                txtName.Clear();
                cbPosition.SelectedIndex = 0;

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
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter a name to delete");
                txtName.Focus();
                return;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String updateQuery = "UPDATE tbadmin SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();

                txtID.Clear();
                txtName.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                cbPosition.SelectedIndex = 0;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter a name to reset new password");
                txtName.Focus();
                return;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                string newpassword = "1234";
                conn.Open();
                String updateQuery = "UPDATE tbadmin SET password = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", txtName.Text +"_" +newpassword);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();

                MessageBox.Show("Your new password is: " + txtName.Text + "_" + newpassword);

                txtID.Clear();
                txtName.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
                cbPosition.SelectedIndex = 0;

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
            string position = "";
            txtConfirmPassword.Enabled = false;
            txtPassword.Enabled = false;
            try
            {
                btnSave.Text = "New";
                btnEdit.Enabled = true;
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                position = selectedRow.Cells[2].Value.ToString();
                if (position.Equals("View Only"))
                {
                    cbPosition.SelectedIndex = 1;
                }
                else if (position.Equals("Create Only"))
                {
                    cbPosition.SelectedIndex = 2;
                }
                else if (position.Equals("Super Admin"))
                {
                    cbPosition.SelectedIndex = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    public partial class FormAmountBed : Form
    {
        private string amountbed_username;
        private string amountbed_role;
        String MySQLConn = "";
        MySqlConnection conn;
        MySqlCommand command;
        DataTable table;
        private string sqlquery = "SELECT * FROM tbamountbed ORDER BY id DESC";
        Boolean buttonSave, buttonEdit, buttonRemove;

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin(amountbed_username, amountbed_role);
            formAdmin.Show();
            this.Hide();
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

        public FormAmountBed(String amountbed_username, String amountbed_role)
        {
            InitializeComponent();
            this.Text = "Position";
            this.amountbed_username = amountbed_username;
            this.amountbed_role = amountbed_role;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                btnSave.Text = "New";
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return;
                }
                else if (txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character or Number enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // check existance data
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(txtName.Text))
                        {
                            MessageBox.Show("This bed's name already exist. Please try again!!!");
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    buttonSave = true;
                    string name = txtName.Text;

                    string query = "INSERT INTO tbamountbed(id, name) VALUES (@id, @name)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("id", "");
                    command.Parameters.AddWithValue("name", name);
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    txtName.Focus();

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
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbamountbed ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
            }
        }

        private void FormAmountBed_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter a name to delete", "Fail", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            else if (txtName.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonRemove = true;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String updateQuery = "UPDATE tbamountambulance SET active = @newValue WHERE id = @id";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);

                command.ExecuteNonQuery();
                TrackUserAction("Remove");
                txtID.Clear();
                txtName.Clear();
                btnSave.Text = "Save";

                txtName.Focus();

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
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
                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text))
                    {
                        MessageBox.Show("This bed's name already assists. Please try again!!!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        conn.Close();
                        return;
                    }
                }
                conn.Open();

                String updateQuery = "UPDATE tbamountbed SET name = @newName WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("id", txtID.Text);

                update_command.ExecuteNonQuery();
                TrackUserAction("Edit");
                txtName.Clear();
                txtName.Focus();

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void FormAmountBed_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            txtID.Enabled = false;
            buttonSave = false;
            buttonEdit = false;
            buttonRemove = false;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbamountbed WHERE active = 1 ORDER BY id DESC", conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbamountbed ORDER BY id DESC LIMIT 1", conn);
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
                    command.Parameters.AddWithValue("uForm", "AmountBed");
                    command.Parameters.AddWithValue("uID", "");
                    command.Parameters.AddWithValue("uName", amountbed_username);
                    command.Parameters.AddWithValue("uRole", amountbed_role);
                    command.Parameters.AddWithValue("pID", txtID.Text);
                    command.Parameters.AddWithValue("pName", txtName.Text);
                    command.Parameters.AddWithValue("aDateTime", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

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
    public partial class FormMedicine : Form
    {
        private string sqlquery = "SELECT * FROM tbmedicine WHERE active = 1 ORDER BY id DESC";
        private string medicine_username;
        private string medicine_role;
        //MySqlConnection conn;
        String MySQLConn = "";
        public FormMedicine(string medicine_username, string medicine_role)
        {
            InitializeComponent();
            this.medicine_username = medicine_username;
            this.medicine_role = medicine_role;
            this.Text = "Medicine Management";
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
            FormManagement formManagement = new FormManagement(medicine_username, medicine_role);
            formManagement.Show();
            this.Hide();
        }

        private void FormMedicine_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            BtnEdit.Enabled = false;
            txtID.Enabled = false;
            BtnRemove.Enabled = false;
            if (medicine_role == "View Only")
            {
                BtnEdit.Enabled = false;
                BtnRemove.Enabled = false;
                BtnSave.Enabled = false;
                BtnReport.Enabled = false;
            }
            else if (medicine_role == "Create Only")
            {
                BtnRemove.Enabled = false;
                BtnReport.Enabled = false;
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
                dataGridView1.Columns[5].Visible = false;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbmedicine ORDER BY id DESC LIMIT 1", conn);
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (BtnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter medicine's name.");
                    return;
                }else if(txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.");
                    return;
                }
                else if (txtQty.Text == "")
                {
                    MessageBox.Show("Please enter qty.");
                    return;
                }else if (txtQty.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character or Character enter.");
                    return;
                }
                else if (txtUnitPrice.Text == "")
                {
                    MessageBox.Show("Please enter medicine's price.");
                    return;
                }else if (txtUnitPrice.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character or Character enter.");
                    return;
                }
                try
                {
                    BtnEdit.Enabled = false;
                    // check duplicated data
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(txtName.Text))
                        {
                            MessageBox.Show("Duplicate Medicine's Name. Please try again!!!");
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string name = txtName.Text;
                    string qty = txtQty.Text;
                    string unitprice = txtUnitPrice.Text;
                    DateTime ExpiryDate = expiryDate.Value;

                    string query = "INSERT INTO tbmedicine(id, name, qty, unitprice, expiryDate) VALUES (@id, @name, @qty, @unitprice, @ExpiryDate)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@qty", qty);
                    command.Parameters.AddWithValue("@unitprice", unitprice);
                    command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                    command.ExecuteNonQuery();

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    txtQty.Clear();
                    txtUnitPrice.Clear();
                    expiryDate.Value = DateTime.Now;
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
                BtnSave.Text = "Save";
                BtnEdit.Enabled = false;
                BtnRemove.Enabled = false;
                txtID.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbmedicine ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                txtQty.Clear();
                txtUnitPrice.Clear();
                expiryDate.Value = DateTime.Now;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtName.ForeColor == System.Drawing.Color.Red)
            {
                MessageBox.Show("No Special Character enter.");
                return;
            }
            BtnSave.Text = "New";
            BtnEdit.Enabled = true;
            BtnRemove.Enabled = true;
            if (medicine_role == "View Only")
            {
                BtnEdit.Enabled = false;
                BtnRemove.Enabled = false;
                BtnSave.Enabled = false;
                BtnReport.Enabled = false;
            }
            else if (medicine_role == "Create Only")
            {
                BtnRemove.Enabled = false;
                BtnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbmedicine WHERE @name = name && active = 1", conn);
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
                    txtQty.Text = table.Rows[0][2].ToString();
                    txtUnitPrice.Text = table.Rows[0][3].ToString();
                    expiryDate.Value = (DateTime)table.Rows[0][4];
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                BtnEdit.Enabled = false;
                BtnSave.Text = "Save";
                txtID.Clear();
                txtName.Clear();
                txtQty.Clear();
                txtUnitPrice.Clear();
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
                    MessageBox.Show("No Special Character enter.");
                    return;
                }else if (txtQty.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character or Character enter.");
                    return;
                }else if (txtUnitPrice.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character or Character enter.");
                    return;
                }
                BtnSave.Text = "New";
                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(txtQty.Text) && row.Cells[3].Value.ToString().Equals(txtUnitPrice.Text) && row.Cells[4].Value.Equals(expiryDate.Value))
                    {
                        MessageBox.Show("This medicine already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                
                String updateQuery = "UPDATE tbmedicine SET name = @newName, qty = @newQty, unitprice = @newUnitPrice, expirydate = @newExpiryDate WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newQty", txtQty.Text);
                update_command.Parameters.AddWithValue("newUnitPrice", txtUnitPrice.Text);
                update_command.Parameters.AddWithValue("id", txtID.Text);
                update_command.Parameters.AddWithValue("newExpiryDate", expiryDate.Value);

                update_command.ExecuteNonQuery();

                txtName.Clear();
                txtQty.Clear();
                txtUnitPrice.Clear();
                expiryDate.Value = DateTime.Now;
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
                conn.Open();
                String updateQuery = "UPDATE tbmedicine SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);
                

                command.ExecuteNonQuery();

                txtID.Clear();
                txtName.Clear();
                txtQty.Clear();
                txtUnitPrice.Clear();
                expiryDate.Value = DateTime.Now;
                BtnEdit.Enabled = false;
                BtnRemove.Enabled = false;

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
                BtnSave.Text = "New";
                BtnEdit.Enabled = true;
                BtnRemove.Enabled = true;
                if (medicine_role == "View Only")
                {
                    BtnEdit.Enabled = false;
                    BtnRemove.Enabled = false;
                    BtnSave.Enabled = false;
                    BtnReport.Enabled = false;
                }
                else if (medicine_role == "Create Only")
                {
                    BtnRemove.Enabled = false;
                    BtnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                txtQty.Text = selectedRow.Cells[2].Value.ToString();
                txtUnitPrice.Text = selectedRow.Cells[3].Value.ToString();
                expiryDate.Value = (DateTime)selectedRow.Cells[4].Value;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormMedicine_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(medicine_username, medicine_role, FormReport._ReportType.Medicine, sqlquery);
            report.Show();
            this.Hide();
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
        }

        private bool QtyAllowed(string text)
        {
            string allowedCharacters = "0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
        }

        private bool PriceAllowed(string text)
        {
            string allowedCharacters = ".0123456789";

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

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (QtyAllowed(txtQty.Text))
            {
                txtQty.BorderStyle = BorderStyle.FixedSingle;
                txtQty.BackColor = System.Drawing.Color.White;
                txtQty.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtQty.BorderStyle = BorderStyle.FixedSingle;
                txtQty.BackColor = System.Drawing.SystemColors.Window;
                txtQty.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            if (PriceAllowed(txtUnitPrice.Text))
            {
                txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
                txtUnitPrice.BackColor = System.Drawing.Color.White;
                txtUnitPrice.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtUnitPrice.BorderStyle = BorderStyle.FixedSingle;
                txtUnitPrice.BackColor = System.Drawing.SystemColors.Window;
                txtUnitPrice.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }
    }
}
    

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

namespace hospital
{
    public partial class FormStaff : Form
    {
        private string staff_username;
        private string staff_role;
        String MySQLConn = "";
        MySqlConnection conn;
        MySqlCommand command;
        byte[] ImageData = null;
        String gender;
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;

        private string sqlquery = "SELECT * FROM tbstaff WHERE active = 1 ORDER BY id DESC";

        private void FormStaff_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public FormStaff(string staff_username, string staff_role)
        {
            InitializeComponent();
            this.staff_username = staff_username;
            this.staff_role = staff_role;
            this.Text = "Staff Management";
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

        private void FormStaff_Load(object sender, EventArgs e)
        {
            Refresh();
            DisplayInComboBox();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog1.Title = "Select an Image File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            if (staff_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (staff_role == "Create Only")
            {
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbstaff WHERE @name = name && active = 1", conn);
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
                    string selectedValue = table.Rows[0][2].ToString();
                    int ind = cbPosition.FindStringExact(selectedValue);
                    cbPosition.SelectedIndex = ind;
                    if (table.Rows[0][3].Equals("Male"))
                    {
                        rbMale.Checked = true;
                    }
                    else
                    {
                        rbFemale.Checked = true;
                    }
                    txtSalary.Text = table.Rows[0][4].ToString();
                    Byte[] img = (Byte[])table.Rows[0][5];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    TrackUserAction("Search");
                }
            }
            catch (Exception ex)
            {
                btnEdit.Enabled = false;
                btnSave.Text = "Save";
                txtID.Clear();
                txtName.Clear();
                cbPosition.SelectedIndex = 0;
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtSalary.Clear();
                pictureBox1.Image = null;
                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.");
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
                if (staff_role == "View Only")
                {
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                    btnReport.Enabled = false;
                }
                else if (staff_role == "Create Only")
                {
                    btnRemove.Enabled = false;
                    btnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();

                string selectedValue = selectedRow.Cells[2].Value.ToString();
                int ind = cbPosition.FindStringExact(selectedValue);
                cbPosition.SelectedIndex = ind;

                if (selectedRow.Cells[3].Value.Equals("Male")) rbMale.Checked = true;
                else rbFemale.Checked = true;

                txtSalary.Text = selectedRow.Cells[4].Value.ToString();

                Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[5].Value;
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            conn = new MySqlConnection(MySQLConn);
            buttonEdit = true;
            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!rbMale.Checked && !rbFemale.Checked)
                {
                    MessageBox.Show("Please select user gender.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtSalary.Text == "")
                {
                    MessageBox.Show("Please enter salary.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtSalary.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character and Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please input Image.");
                    return;
                }
                btnSave.Text = "New";

                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                ImageData = ms.ToArray();

                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && 
                        row.Cells[2].Value.ToString().Equals(cbPosition.SelectedItem) && 
                        row.Cells[3].Value.ToString().Equals(!rbMale.Checked && !rbFemale.Checked) &&
                        row.Cells[4].Value.ToString().Equals(txtSalary.Text) &&
                        row.Cells[5].Value.ToString().Equals(ImageData))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();

                if (rbMale.Checked) gender = "Male";
                else gender = "Female";

                    String updateQuery = "UPDATE tbstaff SET name = @newName, position = @newPosition, gender = @newGender, salary = @newSalary, photo = @newPhoto WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newPosition", cbPosition.SelectedItem.ToString());
                update_command.Parameters.AddWithValue("newGender", gender);
                update_command.Parameters.AddWithValue("newSalary", txtSalary.Text);
                update_command.Parameters.AddWithValue("newPhoto", ImageData);
                update_command.Parameters.AddWithValue("id", txtID.Text);

                update_command.ExecuteNonQuery();
                TrackUserAction("Edit");
                txtName.Clear();
                cbPosition.SelectedIndex = 0;
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtSalary.Clear();
                pictureBox1.Image = null;
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(staff_username, staff_role, FormReport._ReportType.Staff, sqlquery);
            report.Show();
            this.Hide();
            buttonReport = true;
            TrackUserAction("Report");
        }

        private bool SalaryAllowed(string text)
        {
            string allowedCharacters = ".0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
        }

        private void DisplayInComboBox()
        {
            try
            {
                using (conn = new MySqlConnection(MySQLConn))
                {
                    conn.Open();
                    string query = "SELECT name FROM tbposition WHERE active = 1 ORDER BY id DESC";
                    command = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbPosition.Items.Add(reader["name"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            if (SalaryAllowed(txtSalary.Text))
            {
                txtSalary.BorderStyle = BorderStyle.FixedSingle;
                txtSalary.BackColor = System.Drawing.Color.White;
                txtSalary.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtSalary.BorderStyle = BorderStyle.FixedSingle;
                txtSalary.BackColor = System.Drawing.SystemColors.Window;
                txtSalary.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                buttonRemove = true;
                String updateQuery = "UPDATE tbstaff SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();
                TrackUserAction("Remove");

                txtID.Clear();
                txtName.Clear();
                cbPosition.SelectedIndex = 0;
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtSalary.Clear();
                pictureBox1.Image = null;
                btnSave.Text = "Save";

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(staff_username, staff_role);
            formManagement.Show();
            this.Hide();
        }

        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            cbPosition.SelectedIndex = 0;
            btnRemove.Enabled = false;
            if (staff_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (staff_role == "Create Only")
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
                dataGridView1.RowTemplate.Height = 80;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[6].Visible = false;

                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn)dataGridView1.Columns[5];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbstaff ORDER BY id DESC LIMIT 1", conn);
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
            buttonSave = true;
            conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtName.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (cbPosition.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Staff position.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!rbMale.Checked && !rbFemale.Checked)
                {
                    MessageBox.Show("Please select staff gender.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtSalary.Text == "")
                {
                    MessageBox.Show("Please enter staff salary.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtSalary.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character and Character enter.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please input staff Image.");
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
                    string position = cbPosition.SelectedItem.ToString();
                    if (rbMale.Checked) gender = "Male";
                    else gender = "Female";
                    string salary = txtSalary.Text;

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    ImageData = ms.ToArray();

                    string query = "INSERT INTO tbstaff(id, name, position, gender, salary, photo) VALUES (@id, @name, @position, @gender, @salary, @photo)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("id", "");
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("position", position);
                    command.Parameters.AddWithValue("gender", gender);
                    command.Parameters.AddWithValue("salary", salary);
                    command.Parameters.AddWithValue("photo", ImageData);
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();
                    txtName.Clear();
                    cbPosition.SelectedIndex = 0;
                    rbMale.Checked = false;
                    rbFemale.Checked = false;
                    txtSalary.Clear();
                    pictureBox1.Image = null;
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
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbstaff ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                cbPosition.SelectedIndex = 0;
                rbMale.Checked = false;
                rbFemale.Checked = false;
                txtSalary.Clear();
                pictureBox1.Image = null;
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
                    command.Parameters.AddWithValue("uForm", "Staff");
                    command.Parameters.AddWithValue("uID", "");
                    command.Parameters.AddWithValue("uName", staff_username);
                    command.Parameters.AddWithValue("uRole", staff_role);
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

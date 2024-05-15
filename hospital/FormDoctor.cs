using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital
{
    public partial class FormDoctor : Form
    {
        private string doctor_username;
        private string doctor_role;
        String MySQLConn = "";
        byte[] ImageData = null;
        private string sqlquery = "SELECT * FROM tbdoctor WHERE active = 1 ORDER BY id DESC";
        //MySqlConnection conn;

        public FormDoctor(string doctor_username, string doctor_role)
        {
            InitializeComponent();
            this.doctor_username = doctor_username;
            this.doctor_role = doctor_role;
            this.Text = "Doctor Management";
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

        private void FormDoctor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(doctor_username, doctor_role);
            formManagement.Show();
            this.Hide();
        }

        private void FormDoctor_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            btnEdit.Enabled = false;
            txtID.Enabled = false;
            btnRemove.Enabled = false;
            if (doctor_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
                btnBrowse.Enabled = false;
            }
            else if (doctor_role == "Create Only")
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
                dataGridView1.Columns[5].Visible = false;

                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn)dataGridView1.Columns[4];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbdoctor ORDER BY id DESC LIMIT 1", conn);
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
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (btnSave.Text == "Save")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name.");
                    return;
                }
                else if (txtspecialization.Text == "")
                {
                    MessageBox.Show("Please enter specialization.");
                    return;
                }
                else if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please input Image.");
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
                    string phone = txtphone.Text;
                    string specialization = txtspecialization.Text;

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    ImageData = ms.ToArray();

                    string query = "INSERT INTO tbdoctor(id, name, phone, specialization, photo) VALUES (@id, @name, @phone, @specialization, @photo)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("id", "");
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("phone", phone);
                    command.Parameters.AddWithValue("specialization", specialization);
                    command.Parameters.AddWithValue("photo", ImageData);
                    command.ExecuteNonQuery();

                    int id = int.Parse(txtID.Text);
                    int nextID = id + 1;
                    txtID.Text = nextID.ToString();

                    txtName.Clear();
                    txtphone.Clear();
                    txtspecialization.Clear();
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
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbdoctor ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtID.Text = nextId.ToString();

                txtName.Clear();
                txtphone.Clear();
                txtspecialization.Clear();
                pictureBox1.Image = null;
                pictureBox1.BackgroundImage = null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSave.Text = "New";
            btnEdit.Enabled = true;
            if (doctor_role == "View Only")
            {
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                btnReport.Enabled = false;
            }
            else if (doctor_role == "Create Only")
            {
                btnRemove.Enabled = false;
                btnReport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbdoctor WHERE @name = name && active = 1", conn);
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
                    txtphone.Text = table.Rows[0][2].ToString();
                    txtspecialization.Text = table.Rows[0][3].ToString();

                    Byte[] img = (Byte[])table.Rows[0][4];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnEdit.Enabled = false;
                btnSave.Text = "Save";
                txtID.Clear();
                txtName.Clear();
                txtphone.Clear();
                txtspecialization.Clear();
                pictureBox1.Image = null;
                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.");
            }
            finally { conn.Close(); }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String updateQuery = "UPDATE tbdoctor SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtID.Text);
                command.Parameters.AddWithValue("@name", txtName.Text);

                command.ExecuteNonQuery();

                txtID.Clear();
                txtName.Clear();
                txtphone.Clear();
                txtspecialization.Clear();
                pictureBox1.Image = null;
                pictureBox1.BackgroundImage = null;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                btnSave.Text = "New";

                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                ImageData = ms.ToArray();

                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtName.Text) && row.Cells[2].Value.ToString().Equals(txtphone.Text) && row.Cells[3].Value.ToString().Equals(txtspecialization.Text) && row.Cells[4].Value.ToString().Equals(ImageData))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                String updateQuery = "UPDATE tbdoctor SET name = @newName, phone = @newPhone, specialization = @newSpecialization, photo = @newPhoto WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", txtName.Text);
                update_command.Parameters.AddWithValue("newPhone", txtphone.Text);
                update_command.Parameters.AddWithValue("newSpecialization", txtspecialization.Text);
                update_command.Parameters.AddWithValue("newPhoto", ImageData);
                update_command.Parameters.AddWithValue("id", txtID.Text);

                update_command.ExecuteNonQuery();

                txtName.Clear();
                txtphone.Clear();
                txtspecialization.Clear();
                pictureBox1.Image = null;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
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
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(doctor_username, doctor_role, FormReport._ReportType.Doctor, sqlquery);
            report.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnSave.Text = "New";
                btnEdit.Enabled = true;
                btnRemove.Enabled = true;
                if (doctor_role == "View Only")
                {
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                    btnReport.Enabled = false;
                }
                else if (doctor_role == "Create Only")
                {
                    btnRemove.Enabled = false;
                    btnReport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                txtphone.Text = selectedRow.Cells[2].Value.ToString();
                txtspecialization.Text = selectedRow.Cells[3].Value.ToString();

                Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[4].Value;
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

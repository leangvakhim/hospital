using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace hospital
{
    public partial class FormAmbulance : Form
    {
        private string ambulance_username;
        private string ambulance_role;
        //MySqlConnection conn;
        String MySQLConn = "";
        public FormAmbulance(string ambulance_username, string ambulance_role)
        {
            InitializeComponent();
            this.ambulance_username = ambulance_username;
            this.ambulance_role = ambulance_role;
            this.Text = "Ambulance Management";
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
            FormManagement formManagement = new FormManagement(ambulance_username, ambulance_role);
            formManagement.Show();
            this.Hide();
        }
        private void FormAmbulance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }
        private void FormAmbulance_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            btnedit.Enabled = false;
            txtid.Enabled = false;
            if (ambulance_role == "view only")
            {
                btnedit.Enabled = false;
                btndelete.Enabled = false;
                btnsave.Enabled = false;
                btnreport.Enabled = false;
            }
            else if (ambulance_role == "Create Only")
            {
                btnedit.Enabled = false;
                btnreport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("Select * From tbambualnce WHERE active = 1 ORDER BY id DECS", conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.RowTemplate.Height = 60;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbambulance ORDER BY id DESC LIMIT 1", conn);
                object result = command_id.ExecuteScalar();
                int maxID = 0;
                maxID = Convert.ToInt32(result);
                int nextID = maxID + 1;
                txtid.Text = nextID.ToString();

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
        private void btnsave_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            if (btnsave.Text == "Save")
            {
                if (btnstaff.Text == "")
                {
                    MessageBox.Show("Please enter Staff's name.");
                    return;
                }
                else if (btnAm.Text == "")
                {
                    MessageBox.Show("Please enter Ambulance NO.");
                    return
;
                }
                try
                {

                    btnedit.Enabled = false;


                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(btnstaff.Text))
                        {
                            MessageBox.Show("Duplicate Staff's name. Please try again!!!");
                            conn.Close();
                            return;
                        }
                    }
                    conn.Open();
                    string staff = btnstaff.Text;
                    DateTime Departure = dateTimedeparture.Value;
                    DateTime Departuretime = Departure.Date;

                    string query = "INSERT INTO tbambulance(id, name, departure, arrived) VALUES (@id, @name, @departuretime, @arrivedtime)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@staff", staff);
                    command.Parameters.AddWithValue("@departure", Departuretime);
                    command.Parameters.AddWithValue("@arrived", "0001-01-01");
                    command.ExecuteNonQuery();

                    int id = int.Parse(txtid.Text);
                    int nextID = id + 1;
                    txtid.Text = nextID.ToString();

                    cbStaff.SelectedIndex = 0;
                    dateTimedeparture.Value = DateTime.Now;
                    dateTimearrived.Value = DateTime.Now;

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
                btnsave.Text = "Save";
                btnedit.Enabled = false;
                txtid.Enabled = false;
                int maxId = 0;
                conn.Open();
                MySqlCommand command_id = new MySqlCommand("SELECT id FROM tbambulance ORDER BY id DESC LIMIT 1", conn);

                object result = command_id.ExecuteScalar();
                maxId = Convert.ToInt32(result);
                int nextId = maxId + 1;
                txtid.Text = nextId.ToString();

                cbStaff.SelectedIndex = 0;
                dateTimedeparture.ResetText();
                dateTimearrived.ResetText();


            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            btnsave.Text = "New";
            btnedit.Enabled = true;
            dateTimearrived.Enabled = true;
            if (ambulance_role == "View Only")
            {
                btnedit.Enabled = false;
                btndelete.Enabled = false;
                btnsave.Enabled = false;
                btnreport.Enabled = false;
            }
            else if (ambulance_role == "Create Only")
            {
                btndelete.Enabled = false;
                btnreport.Enabled = false;
            }
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbbed WHERE @name = name && active = 1", conn);
                command.Parameters.AddWithValue("name", cbStaff.Text);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count < 0)
                {
                    MessageBox.Show("No data Found!");
                }
                else
                {
                    txtid.Text = table.Rows[0][0].ToString();
                    cbStaff.Text = table.Rows[0][1].ToString();
                    dateTimedeparture.Value = (DateTime)table.Rows[0][2];
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnedit.Enabled = false;
                btnsave.Text = "Save";
                txtid.Clear();
                cbStaff.SelectedIndex = 0;
                dateTimedeparture.Value = DateTime.Today;
                dateTimearrived.Value = DateTime.Today;
                Refresh();
                MessageBox.Show("Name not found in the list. Please try again.");
            }
            finally { conn.Close(); }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                btnsave.Text = "New";

                // check duplicated data
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(cbStaff.Text) && row.Cells[2].Value.ToString().Equals(dateTimedeparture.Value) && row.Cells[3].Value.ToString().Equals(dateTimearrived.Value))
                    {
                        MessageBox.Show("This user already assists. Please try again!!!");
                        conn.Close();
                        return;
                    }
                }
                conn.Open();
                DateTime departure = dateTimedeparture.Value;
                DateTime departureDate = departure.Date;
                DateTime arrived = dateTimearrived.Value;
                DateTime arrivedDate = arrived.Date;
                String updateQuery = "UPDATE tbambulance SET name = @newName, departure = @newdeparture, arrived = @newarrived WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newName", cbStaff.Text);
                update_command.Parameters.AddWithValue("newdeparture", departureDate);
                update_command.Parameters.AddWithValue("newarrived", arrivedDate);
                update_command.Parameters.AddWithValue("id", txtid.Text);

                update_command.ExecuteNonQuery();

                cbStaff.SelectedIndex = 0;
                dateTimedeparture.Value = DateTime.Now;
                dateTimearrived.Value = DateTime.Now;

                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
        private void btndelete_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                String updateQuery = "UPDATE tbambulance SET active = @newValue WHERE id = @id || name = @name";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtid.Text);
                command.Parameters.AddWithValue("@name", cbStaff.Text);

                command.ExecuteNonQuery();

                txtid.Clear();
                cbStaff.SelectedIndex = 0;
                dateTimedeparture.Value = DateTime.Today;
                dateTimearrived.Value = DateTime.Today;

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
                btnsave.Text = "New";
                btnedit.Enabled = true;
                dateTimedeparture.Enabled = true;
                if (ambulance_role == "View Only")
                {

                    btnedit.Enabled = false;
                    btndelete.Enabled = false;
                    btnsave.Enabled = false;
                    btnreport.Enabled = false;
                }
                else if (ambulance_role == "Create Only")
                {
                    btndelete.Enabled = false;
                    btnreport.Enabled = false;
                }
                int index = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtid.Text = selectedRow.Cells[0].Value.ToString();
                cbStaff.Text = selectedRow.Cells[1].Value.ToString();
                dateTimedeparture.Value = (DateTime)selectedRow.Cells[2].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private Bitmap bmp;
        //private int WIDTH = 100, HEIGHT = 100, secHAND = 45, minHAND = 30, hrHAND = 18;
        //private int cx, cy;

        private void FormAmbulance_Load_1(object sender, EventArgs e)
        {
            clock();
        }

        private const int WIDTH = 188;
        private const int HEIGHT = 181;
        private const int cx = WIDTH / 2;
        private const int cy = HEIGHT / 2;
        private const int secHAND = 70;
        private const int minHAND = 60;
        private const int hrHAND = 50;

        private Bitmap bmp;

        private void clock()
        {
            Timer t = new Timer();
            bmp = new Bitmap(WIDTH, HEIGHT);
            this.BackColor = Color.White;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.DrawEllipse(new Pen(Color.Black), 0, 0, WIDTH - 1, HEIGHT - 1);
                g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(cx - 10, 0));
                g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(WIDTH - 20, cy - 10)); ;
                g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(cx - 10, HEIGHT - 20)); ;
                g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, cy - 10));



                DateTime now = DateTime.Now;
                int ss = now.Second;
                int mm = now.Minute;
                int hh = now.Hour;

                DrawHand(g, Color.Red, ss, secHAND, 6);
                DrawHand(g, Color.Black, mm, minHAND, 6);
                DrawHand(g, Color.Gray, (hh % 12) * 30 + mm / 2, hrHAND, 30);

                time.Image = bmp;
                this.Text = "Analog Clock-" + now.ToString("HH:mm:ss");
            }
        }
        private void DrawHand(Graphics g, Color color, int val, int length, int angleFactor)
        {
            int[] coord = new int[2];
            int angle = val * angleFactor;

            if (angle >= 0 && angle <= 180)
            {
                coord[0] = cx + (int)(length * Math.Sin(Math.PI * angle / 180));
                coord[1] = cy - (int)(length * Math.Cos(Math.PI * angle / 180));
            }
            else
            {
                coord[0] = cx - (int)(length * -Math.Sin(Math.PI * angle / 180));
                coord[1] = cy - (int)(length * Math.Cos(Math.PI * angle / 180));
            }

            g.DrawLine(new Pen(color, color == Color.Red ? 1f : 2f), new Point(cx, cy), new Point(coord[0], coord[1]));
        }



    }
}
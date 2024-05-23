using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private const int WIDTH = 180;
        private const int HEIGHT = 180;
        private const int cx = WIDTH / 2;
        private const int cy = HEIGHT / 2;
        private const int secHAND = 70;
        private const int minHAND = 60;
        private const int hrHAND = 45;
        private Bitmap bmp;
        private string ambulance_username;
        private string ambulance_role;
        String MySQLConn = "";
        MySqlConnection conn;
        MySqlCommand command;
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        private string sqlquery = "SELECT * FROM tbambulance WHERE active = 1 ORDER BY id DESC";

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
            DisplayInComboBox();
            clock();
        }
        
        private void Refresh()
        {
            btnedit.Enabled = false;
            txtid.Enabled = false;
            btndelete.Enabled = false;
            cbStaff.SelectedIndex = 0;
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
            conn = new MySqlConnection(MySQLConn);
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
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            DateTime Departure, Arrived;
            if (btnsave.Text == "Save")
            {
                if(cbStaff.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Staff name.");
                    return;
                }
                else if (txtAm.Text == "")
                {
                    MessageBox.Show("Please enter Ambulance NO.");
                    return;
                }else if(txtAm.ForeColor == System.Drawing.Color.Red)
                {
                    MessageBox.Show("No Special Character enter.");
                    return;
                }
                try
                {
                    btnedit.Enabled = false;
                    buttonSave = true;
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
                    string ambulanceno = txtAm.Text;
                    string name = cbStaff.SelectedItem.ToString();
                    Departure = dateTimedeparture.Value;
                    Arrived = dateTimearrived.Value;

                    string query = "INSERT INTO tbambulance (id, ambulanceNo, staffName, departureTime, arriveTime) VALUES (@id, @ambulanceno, @name, @departuretime, @arrivedtime)";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@ambulanceno", ambulanceno);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@departuretime", Departure);
                    command.Parameters.AddWithValue("@arrivedtime", Arrived);
                    command.ExecuteNonQuery();
                    TrackUserAction("Save");

                    int id = int.Parse(txtid.Text);
                    int nextID = id + 1;
                    txtid.Text = nextID.ToString();

                    txtAm.Clear();
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

                txtAm.Clear();
                cbStaff.SelectedIndex = 0;
                dateTimedeparture.Value = DateTime.Now;
                dateTimearrived.Value = DateTime.Now;
            }
        }
        
        private void btnsearch_Click(object sender, EventArgs e)
        {
            btnsave.Text = "New";
            btnedit.Enabled = true;
            dateTimearrived.Enabled = true;
            btndelete.Enabled = true;
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
            buttonSearch = true;
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM tbambulance WHERE @ambulanceNo = ambulanceno && active = 1", conn);
                command.Parameters.AddWithValue("ambulanceno", txtAm.Text);
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
                    txtAm.Text = table.Rows[0][1].ToString();
                    string selectedValue = table.Rows[0][2].ToString();
                    int ind = cbStaff.FindStringExact(selectedValue);
                    cbStaff.SelectedIndex = ind;
                    dateTimedeparture.Value = (DateTime)table.Rows[0][3];
                    dateTimearrived.Value = (DateTime)table.Rows[0][4];
                    TrackUserAction("Search");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btnedit.Enabled = false;
                btnsave.Text = "Save";
                btndelete.Enabled = false;
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
                string ambulanceno = txtAm.Text;
                string name = cbStaff.SelectedItem.ToString();
                DateTime departure = dateTimedeparture.Value;
                DateTime arrived = dateTimearrived.Value;
                String updateQuery = "UPDATE tbambulance SET ambulanceNo = @newambulanceNo, staffName = @newName, departureTime = @newdeparture, arriveTime = @newarrived WHERE id = @id";
                MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                update_command.Parameters.AddWithValue("newambulanceNo", ambulanceno);
                update_command.Parameters.AddWithValue("newName", name);
                update_command.Parameters.AddWithValue("newdeparture", departure);
                update_command.Parameters.AddWithValue("newarrived", arrived);
                update_command.Parameters.AddWithValue("id", txtid.Text);

                update_command.ExecuteNonQuery();
                TrackUserAction("Edit");

                cbStaff.SelectedIndex = 0;
                txtAm.Clear();
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
                String updateQuery = "UPDATE tbambulance SET active = @newValue WHERE id = @id";
                MySqlCommand command = new MySqlCommand(updateQuery, conn);

                command.Parameters.AddWithValue("@newValue", 0);
                command.Parameters.AddWithValue("@id", txtid.Text);

                command.ExecuteNonQuery();
                TrackUserAction("Remove");

                txtAm.Clear();
                cbStaff.SelectedIndex = 0;
                dateTimedeparture.Value = DateTime.Today;
                dateTimearrived.Value = DateTime.Today;
                btnsave.Text = "Save";

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
                btndelete.Enabled = true;
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
                txtAm.Text = selectedRow.Cells[1].Value.ToString();
                string selectedValue = selectedRow.Cells[2].Value.ToString();
                int ind = cbStaff.FindStringExact(selectedValue);
                cbStaff.SelectedIndex = ind;
                dateTimedeparture.Value = (DateTime)selectedRow.Cells[3].Value;
                dateTimearrived.Value = (DateTime)selectedRow.Cells[4].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(ambulance_username, ambulance_role, FormReport._ReportType.Ambulance, sqlquery);
            buttonReport = true;
            report.Show();
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
                    command.Parameters.AddWithValue("uName", ambulance_username);
                    command.Parameters.AddWithValue("uRole", ambulance_role);
                    if (userAction.Equals("Report"))
                    {
                        command.Parameters.AddWithValue("pID", "");
                        command.Parameters.AddWithValue("pName", "");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("pID", txtid.Text);
                        command.Parameters.AddWithValue("pName", cbStaff.Text);
                    }
                    command.Parameters.AddWithValue("aDateTime", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtAm_TextChanged(object sender, EventArgs e)
        {
            if (ContainsSpecialCharacters(txtAm.Text))
            {
                txtAm.BorderStyle = BorderStyle.FixedSingle;
                txtAm.BackColor = System.Drawing.Color.White;
                txtAm.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtAm.BorderStyle = BorderStyle.FixedSingle;
                txtAm.BackColor = System.Drawing.SystemColors.Window;
                txtAm.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void DisplayInComboBox()
        {
            try
            {
                using (conn = new MySqlConnection(MySQLConn))
                {
                    conn.Open();
                    string query = "SELECT name FROM tbstaff WHERE position = 'Driver' && active = 1 ORDER BY id DESC";
                    command = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cbStaff.Items.Add(reader["name"].ToString());
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ContainsSpecialCharacters(string text)
        {
            string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return text.Any(c => !allowedCharacters.Contains(c));
        }

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
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                //g.DrawEllipse(new Pen(Color.Black), 0, 0, WIDTH - 1, HEIGHT - 1);

                for (int i = 1; i <= 12; i++)
                {
                    double angle = Math.PI / 6 * (i - 3);
                    int x = (int)(cx + Math.Cos(angle) * (cx - 20));
                    int y = (int)(cy + Math.Sin(angle) * (cy - 20));
                    g.DrawString(i.ToString(), new Font("Arial", 8), Brushes.Black, new PointF(x, y));
                }

                DateTime now = DateTime.Now;
                int ss = now.Second;
                int mm = now.Minute;
                int hh = now.Hour;

                double secAngle = Math.PI / 30 * ss;
                double minAngle = Math.PI / 30 * mm + Math.PI / 1800 * ss; 
                double hrAngle = Math.PI / 6 * (hh % 12) + Math.PI / 360 * mm; 

                DrawHand(g, Color.Red, secAngle, secHAND, 6);
                DrawHand(g, Color.Black, minAngle, minHAND, 6);
                DrawHand(g, Color.Green, hrAngle, hrHAND, 30);

                time.Image = bmp;
                this.Text = "Analog Clock - " + now.ToString("HH:mm:ss");
            }
        }

        private void DrawHand(Graphics g, Color color, double angle, int length, int thickness)
        {
            int x = cx + (int)(length * Math.Sin(angle));
            int y = cy - (int)(length * Math.Cos(angle));

            g.DrawLine(new Pen(color, thickness/6), new Point(cx, cy), new Point(x, y));
        }
    }
}
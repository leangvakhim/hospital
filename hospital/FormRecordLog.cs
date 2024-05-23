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
using System.Xml.Linq;

namespace hospital
{
    public partial class FormRecordLog : Form
    {
        Boolean buttonSave, buttonEdit, buttonRemove, buttonReport, buttonSearch;
        private string recordlog_username;
        private string recordlog_role;
        String MySQLConn = "";
        MySqlConnection conn;
        MySqlCommand command;
        DataTable table;
        private string sqlquery = "SELECT * FROM tbrecord ORDER BY userID DESC";
        public FormRecordLog(string recordlog_username, string recordlog_role)
        {
            InitializeComponent();
            this.Text = "Record Log";
            this.recordlog_username = recordlog_username;
            this.recordlog_role = recordlog_role;
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

        private void FormRecordLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin(recordlog_username, recordlog_role);
            formAdmin.Show();
            this.Hide();
        }

        private void FormRecordLog_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                command = new MySqlCommand(sqlquery, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "Position";
                dataGridView1.Columns[3].HeaderText = "Action";
                dataGridView1.Columns[4].HeaderText = "Form";
                dataGridView1.Columns[7].HeaderText = "Date&Time";

                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.DataSource = table;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime StartDate = startDate.Value.Date;
            DateTime EndDate = endDate.Value.Date.AddDays(1).AddTicks(-1);

            string searchQuery = "SELECT * FROM tbrecord WHERE actionDateTime BETWEEN @StartDate AND @EndDate ORDER BY userID DESC";

            //MessageBox.Show(StartDate.ToString() + "\n" + EndDate.ToString());
            using (MySqlConnection conn = new MySqlConnection(MySQLConn))
            {
                using (MySqlCommand command = new MySqlCommand(searchQuery, conn))
                {
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);

                    try
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Setting DataGridView properties
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = table;

                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[1].HeaderText = "Name";
                        dataGridView1.Columns[2].HeaderText = "Position";
                        dataGridView1.Columns[3].HeaderText = "Action";
                        dataGridView1.Columns[4].HeaderText = "Form";
                        dataGridView1.Columns[7].HeaderText = "Date&Time";

                        dataGridView1.RowTemplate.Height = 30;
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.ReadOnly = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport(recordlog_username, recordlog_role, FormReport._ReportType.Record, sqlquery);
            buttonReport = true;
            report.Show();
            this.Hide();
            /*TrackUserAction("Report");*/
        }

        /*private void TrackUserAction(string userAction)
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
        }*/
    }
}

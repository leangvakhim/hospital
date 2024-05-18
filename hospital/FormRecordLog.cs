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
    public partial class FormRecordLog : Form
    {
        private string recordlog_username;
        private string recordlog_role;
        String MySQLConn = "";
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
    }
}

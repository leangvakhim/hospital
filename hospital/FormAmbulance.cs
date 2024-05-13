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
    }
}

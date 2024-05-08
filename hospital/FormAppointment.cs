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
    public partial class FormAppointment : Form
    {
        private string appointment_username;
        private string appointment_role;
        MySqlConnection conn;
        String MySQLConn = "";
        public FormAppointment(string appointment_username, string appointment_role)
        {
            InitializeComponent();
            this.appointment_username = appointment_username;
            this.appointment_role = appointment_role;
            this.Text = "Appointment day Management";
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
            FormManagement formManagement = new FormManagement(appointment_username, appointment_role);
            formManagement.Show();
            this.Hide();
        }
    }
}

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
    public partial class FormStaff : Form
    {
        private string staff_username;
        private string staff_role;
        MySqlConnection conn;
        String MySQLConn = "";
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormManagement formManagement = new FormManagement(staff_username, staff_role);
            formManagement.Show();
            this.Hide();
        }
    }
}

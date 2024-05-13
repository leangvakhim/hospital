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
    }
}

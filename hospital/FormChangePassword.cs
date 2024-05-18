using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace hospital
{
    public partial class FormChangePassword : Form
    {
        String MySQLConn = "";
        MySqlConnection conn;
        MySqlCommand command;
        Boolean correct;
        public FormChangePassword()
        {
            InitializeComponent();
            this.Text = "Change Password";
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

        private void FormChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkUser()
        {
            conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT name, password FROM tbadmin WHERE active = 1", conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string Name = reader.GetString("name");
                    string Password = reader.GetString("password");
                    if (txtusername.Text == Name && txtoldpassoword.Text == Password)
                    {
                        correct = true;
                        break;
                    }
                    correct = false;
                } 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); correct = false; }
            finally { conn.Close(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            checkUser();
            try
            {
                if(correct == true)
                {
                    if(txtnewpassword.Text == txtconfirmpassword.Text)
                    {
                        conn.Open();
                        String updateQuery = "UPDATE tbadmin SET password = @newPassword WHERE name = @name";
                        MySqlCommand update_command = new MySqlCommand(updateQuery, conn);

                        update_command.Parameters.AddWithValue("newPassword", txtconfirmpassword.Text);
                        update_command.Parameters.AddWithValue("name", txtusername.Text);

                        update_command.ExecuteNonQuery();
                        MessageBox.Show("Your Password has been changed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtusername.Clear();
                        txtconfirmpassword.Clear();
                        txtnewpassword.Clear();
                        txtoldpassoword.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Confirm Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { conn.Close(); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }
    }
}

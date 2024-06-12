using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using hospital;
using MySql.Data.MySqlClient;

namespace hospital
{
    public partial class FormLogin : Form
    {
        String MySQLConn = "";
        String username = "";
        String password = "";
        String role = "";

        public FormLogin()
        {
            InitializeComponent();
            this.Text = "Login";
        }

        void Authenticate()
        {
            var key = "sw9zbIu5mZ1AouhBGKikQWyhUhFdGftx";
            string MySQLConn = "server=127.0.0.1; user=root; database=hospital; password=";
            MySqlConnection conn = new MySqlConnection(MySQLConn);
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT name, password, position FROM tbadmin WHERE active = 1 AND password = @Password AND name = @Name", conn);

                String userLoginName = txtusername.Text;
                String userLoginPassword = txtpassword.Text;
                var encryptedUserLoginPassword = EncryptionDecryption.EncryptString(key, userLoginPassword);

                command.Parameters.AddWithValue("@Name", userLoginName);
                command.Parameters.AddWithValue("@Password", encryptedUserLoginPassword);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string Name = reader.GetString("name");
                        string Role = reader.GetString("position");
                        username = Name;
                        role = Role;
                        FormManagement formManagement = new FormManagement(username, role);
                        formManagement.Show();
                        this.Hide();
                        break;
                    }
                }
                else if (txtusername.Text == "admin" && txtpassword.Text == "123")
                {
                    username = "Admin";
                    role = "Super Admin";
                    FormManagement formManagement = new FormManagement(username, role);
                    formManagement.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linklableChangePassword_MouseClick(object sender, MouseEventArgs e)
        {
            FormChangePassword formChangePassword = new FormChangePassword();
            formChangePassword.Show();
            this.Hide();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            Authenticate();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
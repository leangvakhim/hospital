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
    public partial class FormChangePassword : Form
    {
        public FormChangePassword()
        {
            InitializeComponent();
        }

        private void FormChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }
    }
}

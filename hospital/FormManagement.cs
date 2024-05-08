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
    public partial class FormManagement : Form
    {
        private string username;
        private string role;
        public FormManagement(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;
            this.Text = "Hospital Management";
            if (role != "Super Admin")
            {
                btnAdmin.Enabled = false;
            }
        }

        private void FormManagement_Load(object sender, EventArgs e)
        {
            labelWelcome.Text = "Welcome" + "\n" + username;
        }

        private void FormManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            string doctor_username = username;
            string doctor_role = role;
            doctor_username = this.username;
            doctor_role = this.role;
            FormDoctor formDoctor = new FormDoctor(doctor_username, doctor_role);
            formDoctor.Show();
            this.Hide();
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            string patient_username = username;
            string patient_role = role;
            patient_username = this.username;
            patient_role = this.role;
            FormPatient formPatient = new FormPatient(patient_username, patient_role);
            formPatient.Show();
            this.Hide();
        }

        private void btnBed_Click(object sender, EventArgs e)
        {
            string bed_username = username;
            string bed_role = role;
            bed_username = this.username;
            bed_role = this.role;
            FormBed formBed = new FormBed(bed_username, bed_role);
            formBed.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            string admin_username = username;
            string admin_role = role;
            admin_username = this.username;
            admin_role = this.role;
            FormAdmin formAdmin = new FormAdmin(admin_username, admin_role);
            formAdmin.Show();
            this.Hide();
        }

        private void btnmedicine_Click(object sender, EventArgs e)
        {
            string medicine_username = username;
            string medicine_role = role;
            medicine_username = this.username;
            medicine_role = this.role;
            FormMedicine formMedicine = new FormMedicine(medicine_username, medicine_role);
            formMedicine.Show();
            this.Hide();
        }

        private void btnappointment_Click(object sender, EventArgs e)
        {
            string appointment_username = username;
            string appointment_role = role;
            appointment_username = this.username;
            appointment_role = this.role;
            FormAppointment formAppointment = new FormAppointment(appointment_username, appointment_role);
            formAppointment.Show();
            this.Hide();
        }

        private void btnambulance_Click(object sender, EventArgs e)
        {
            string ambulance_username = username;
            string ambulance_role = role;
            ambulance_username = this.username;
            ambulance_role = this.role;
            FormAmbulance formAmbulance = new FormAmbulance(ambulance_username, ambulance_role);
            formAmbulance.Show();
            this.Hide();
        }

        private void btnstaff_Click(object sender, EventArgs e)
        {
            string staff_username = username;
            string staff_role = role;
            staff_username = this.username;
            staff_role = this.role;
            FormStaff formStaff = new FormStaff(staff_username, staff_role);
            formStaff.Show();
            this.Hide();
        }
    }
}
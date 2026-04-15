using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace e_commerce_NYC
{
    public partial class uc_patient_add : Form
    {
        public uc_patient_add()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (
       string.IsNullOrWhiteSpace(first_name.Text) ||
       string.IsNullOrWhiteSpace(last_name.Text) ||
       string.IsNullOrWhiteSpace(phone.Text) ||
       string.IsNullOrWhiteSpace(email.Text) ||
       string.IsNullOrWhiteSpace(adress.Text) ||
       string.IsNullOrWhiteSpace(city.Text) ||
       string.IsNullOrWhiteSpace(country.Text) ||
       string.IsNullOrWhiteSpace(idnp.Text) )
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            try
            {
                Patient newPatient = new Patient
                {

                    first_name = first_name.Text,
                    last_name = last_name.Text,
                    date_birth = guna2DateTimePicker1.Value,
                    gender = guna2ComboBox1.SelectedItem.ToString(),
                    phone = phone.Text,
                    email = email.Text,
                    adress = adress.Text,
                    city = city.Text,
                    country = country.Text,
                    idnp = idnp.Text,
                    is_active = guna2CheckBox1.Checked
                };
                patient_action_sql repo = new patient_action_sql();
                repo.AddPerson(newPatient);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the patient: " + ex.Message);
                return;
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }
    }
}

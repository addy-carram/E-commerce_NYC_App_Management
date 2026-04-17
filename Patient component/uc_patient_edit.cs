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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace e_commerce_NYC.Patient_component
{
    public partial class uc_patient_edit : Form
    {
        private int patientId;
        public uc_patient_edit(int id)
        {
            InitializeComponent(); 
             patientId = id;

            LoadPatients();
        }
        private void LoadPatients()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Patient WHERE id_patient = @id", conn);

                cmd.Parameters.AddWithValue("@id", patientId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id.Text = reader["id_patient"].ToString();
                    id.ReadOnly = true;

                    first_name.Text = reader["first_name"].ToString();
                    last_name.Text = reader["last_name"].ToString();
                    guna2DateTimePicker1.Value = Convert.ToDateTime(reader["date_of_birth"]);
                    phone.Text = reader["phone"].ToString();
                    email.Text = reader["email"].ToString();
                    adress.Text = reader["address"].ToString();
                    city.Text = reader["city"].ToString();
                    country.Text = reader["country"].ToString();
                    idnp.Text = reader["idnp"].ToString();
                    guna2CheckBox1.Checked = Convert.ToBoolean(reader["is_active"]);
                }
                else
                {
                    MessageBox.Show("Patient not found!");
                    this.Close();
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Patient emp = new Patient
            {
                id = patientId,
                first_name = first_name.Text,
                last_name = last_name.Text,
                date_birth = guna2DateTimePicker1.Value,
                phone = phone.Text,
                email = email.Text,
                adress = adress.Text,
                city = city.Text,
                country = country.Text,
                idnp = idnp.Text,
                is_active = guna2CheckBox1.Checked
            };

            patient_action_sql repo = new patient_action_sql();
            repo.UpdatePatient(emp);

            MessageBox.Show("Updated!");


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
    }
}

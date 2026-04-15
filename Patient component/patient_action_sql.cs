
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace e_commerce_NYC
{
    public class patient_action_sql
    {
        public void AddPerson(Patient patient)
        {
            string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Patient (first_name, last_name,date_of_birth,gender,phone,email,address,city,country,idnp,is_active)" +
                    "Values(@first_name,@last_name,@date_birth,@gender,@phone,@email,@adress,@city,@country,@idnp,@is_active)", conn);
                cmd.Parameters.AddWithValue("@first_name", patient.first_name);
                cmd.Parameters.AddWithValue("@last_name", patient.last_name);
                cmd.Parameters.AddWithValue("@date_birth", patient.date_birth);
                cmd.Parameters.AddWithValue("@gender", patient.gender);
                cmd.Parameters.AddWithValue("@phone", patient.phone);
                cmd.Parameters.AddWithValue("@email", patient.email);
                cmd.Parameters.AddWithValue("@adress", patient.adress);
                cmd.Parameters.AddWithValue("@city", patient.city);
                cmd.Parameters.AddWithValue("@country", patient.country);
                cmd.Parameters.AddWithValue("@idnp", patient.idnp);
                cmd.Parameters.AddWithValue("@is_active", patient.is_active);
                cmd.ExecuteNonQuery();



            }
        }
        public void DeletePerson(int id)
        {
            try
            {
                string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connSt))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Patient WHERE id_patient=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("eror" + ex.Message);
            }
        }
        public void UpdatePatient(Patient p)
        {
            string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            UPDATE Patient
            SET first_name=@fn,
                last_name=@ln,
                date_of_birth=@br,
                phone=@ph,
                email=@em,
                address=@add,
                city=@city,
                country=@country,
                idnp=@idnp,
                is_active=@is_active
            WHERE id_patient=@id", conn);

                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@fn", p.first_name);
                cmd.Parameters.AddWithValue("@ln", p.last_name);
                cmd.Parameters.AddWithValue("@br", p.date_birth);
                cmd.Parameters.AddWithValue("@ph", p.phone);
                cmd.Parameters.AddWithValue("@em", p.email);

                cmd.Parameters.AddWithValue("@add", p.adress);
                cmd.Parameters.AddWithValue("@city", p.city);
                cmd.Parameters.AddWithValue("@country", p.country);
                cmd.Parameters.AddWithValue("@idnp", p.idnp);
                cmd.Parameters.AddWithValue("@is_active", p.is_active);

                cmd.ExecuteNonQuery();
            }
        }

    } }
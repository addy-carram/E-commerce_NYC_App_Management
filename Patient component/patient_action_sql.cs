
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

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

        } }
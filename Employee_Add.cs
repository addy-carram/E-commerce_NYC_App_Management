
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace e_commerce_NYC
{
    internal class Employee_Add
    {
        string connStr =
        @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

        public void AddPerson(Employee p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                
                            SqlCommand cmd = new SqlCommand(
                   "INSERT INTO Employee (first_name,last_name,id_role,phone,email,hire_date,salary,is_active) VALUES (@fn,@ln,@role,@ph,@em,@hd,@sal,@is_active)", conn);
                         
                            cmd.Parameters.AddWithValue("@fn", p.first_name);
                            cmd.Parameters.AddWithValue("@ln", p.last_name);
                            cmd.Parameters.AddWithValue("@role", p.role);
                            cmd.Parameters.AddWithValue("@ph", p.phone);
                            cmd.Parameters.AddWithValue("@em", p.email);
                            cmd.Parameters.AddWithValue("@hd", DateTime.Now);
                            cmd.Parameters.AddWithValue("@sal", p.salary);
                            cmd.Parameters.AddWithValue("@is_active", p.is_active);

                            cmd.ExecuteNonQuery();
                        
                }
               
            }
        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Employee WHERE id_employee = @id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

        }
        public void UpdateEmployee(Employee p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            UPDATE Employee
            SET first_name=@fn,
                last_name=@ln,
                id_role=@role,
                phone=@ph,
                email=@em,
                salary=@sal,
                is_active=@is_active
            WHERE id_employee=@id", conn);

                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@fn", p.first_name);
                cmd.Parameters.AddWithValue("@ln", p.last_name);
                cmd.Parameters.AddWithValue("@role", p.role);
                cmd.Parameters.AddWithValue("@ph", p.phone);
                cmd.Parameters.AddWithValue("@em", p.email);
                cmd.Parameters.AddWithValue("@sal", p.salary);
                cmd.Parameters.AddWithValue("@is_active", p.is_active);

                cmd.ExecuteNonQuery();
            }
        }
    }

    }

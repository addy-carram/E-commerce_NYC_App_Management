
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace e_commerce_NYC.Order_component
{
    public class order_action_sql
    {
        string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
        public void AddOrder(Order o)
        {
            int deleteid;

            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to add?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "Insert into Order_Table(id_patient,id_product,id_employee,order_date,id_order_status,total_amount,id_payment_status,id_payment_method,notes)" +
                            "Values(@id_patient,@id_product,@id_employee,@order_date,@id_order_status,@total_amount,@id_payment_status,@id_payment_method,@notes)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id_patient", o.id_patient);
                        cmd.Parameters.AddWithValue("@id_product", o.id_product);
                        cmd.Parameters.AddWithValue("@id_employee", o.id_employee);
                        cmd.Parameters.AddWithValue("@order_date", o.order_date);
                        cmd.Parameters.AddWithValue("@id_order_status", o.id_order_status);
                        cmd.Parameters.AddWithValue("@total_amount", o.total_amount);
                        cmd.Parameters.AddWithValue("@id_payment_status", o.id_payment_status);
                        cmd.Parameters.AddWithValue("@id_payment_method", o.id_payment_method);
                        cmd.Parameters.AddWithValue("@notes", o.notes);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }
        public void DeleteOrder(int id)
        {

            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "DELETE FROM Order_Table WHERE id_order=@id";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }
        public void UpdateOrder(Order o) {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to update?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = @"UPDATE Order_Table
                                 SET id_patient = @id_patient,
                                     id_product = @id_product,
                                     id_employee = @id_employee,
                                     order_date = @order_date,
                                     id_order_status = @id_order_status,
                                     total_amount = @total_amount,
                                     id_payment_status = @id_payment_status,
                                     id_payment_method = @id_payment_method,
                                     notes = @notes
                                 WHERE id_order = @id_order";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@id_order", o.id_order);
                        cmd.Parameters.AddWithValue("@id_patient", o.id_patient);
                        cmd.Parameters.AddWithValue("@id_product", o.id_product);
                        cmd.Parameters.AddWithValue("@id_employee", o.id_employee);
                        cmd.Parameters.AddWithValue("@order_date", o.order_date);
                        cmd.Parameters.AddWithValue("@id_order_status", o.id_order_status);
                        cmd.Parameters.AddWithValue("@total_amount", o.total_amount);
                        cmd.Parameters.AddWithValue("@id_payment_status", o.id_payment_status);
                        cmd.Parameters.AddWithValue("@id_payment_method", o.id_payment_method);
                        cmd.Parameters.AddWithValue("@notes", o.notes);

                        int rows = cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }



        }
        public void CancelOrder(int id)
        {
            try
            {
                
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "UPDATE Order_Table SET id_order_status = 5 WHERE id_order = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }
        public void UNCancelOrder(int id)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE Order_Table SET id_order_status = 3 WHERE id_order = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }
    }

        
}
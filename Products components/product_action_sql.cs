
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace e_commerce_NYC.Products_components
{
    public class product_action_sql
    {
        public void AddProduct(Product p)
        {
            string connSr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connSr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Product (producer, model_name, id_product_type, total_price, stock_quantity,is_active) VALUES (@producer, @model_name, @id_type, @total_price, @stock_quantity, @is_active)", conn);
                cmd.Parameters.AddWithValue("@producer", p.producer);
                cmd.Parameters.AddWithValue("@model_name", p.model_name);
                cmd.Parameters.AddWithValue("@id_type", p.id_type);
                cmd.Parameters.AddWithValue("@total_price", p.total_price);
                cmd.Parameters.AddWithValue("@stock_quantity", p.stock_quantity);
                cmd.Parameters.AddWithValue("@is_active", p.is_active);
                cmd.ExecuteNonQuery();
            }

        }
        public void DeleteProduct(int id)
        {
            string connSr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connSr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE id_product=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product p)
        {
            string connSr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connSr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET producer=@producer, model_name=@model_name, id_product_type=@id_type, total_price=@total_price, stock_quantity=@stock_quantity, is_active=@is_active WHERE id_product=@id", conn);
                cmd.Parameters.AddWithValue("@id", p.id);
                cmd.Parameters.AddWithValue("@producer", p.producer);
                cmd.Parameters.AddWithValue("@model_name", p.model_name);
                cmd.Parameters.AddWithValue("@id_type", p.id_type);
                cmd.Parameters.AddWithValue("@total_price", p.total_price);
                cmd.Parameters.AddWithValue("@stock_quantity", p.stock_quantity);
                cmd.Parameters.AddWithValue("@is_active", p.is_active);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
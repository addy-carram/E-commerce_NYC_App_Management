using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace e_commerce_NYC.Products_components
{
    public partial class uc_product_edit : Form
    {
        private int productId;
        public uc_product_edit(int id)
        {
            InitializeComponent();
            productId = id;
            LoadMaterials();
            LoadProducts();
        }
        private void LoadMaterials()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT id_product_type, type_name FROM Product_Type", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                combomaterial.DataSource = dt;
                combomaterial.DisplayMember = "type_name";
                combomaterial.ValueMember = "id_product_type";
            }
        }
        private void LoadProducts()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM Product WHERE id_product = @productId", conn);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id.Text = reader["id_product"].ToString();
                        id.ReadOnly = true;
                        producer.Text = reader["producer"].ToString();
                        model_name.Text = reader["model_name"].ToString();
                        combomaterial.SelectedValue = reader["id_product_type"];
                        total_price.Text = reader["total_price"].ToString();
                        stock_quantity.Text = reader["stock_quantity"].ToString();
                        guna2CheckBox1.Checked = Convert.ToBoolean(reader["is_active"]);
                    }
                    else
                    {
                        MessageBox.Show("Product not found!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product: " + ex.Message);
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
                        Product p = new Product
            {
                id = Convert.ToInt32(id.Text),
                producer = producer.Text,
                model_name = model_name.Text,
                id_type = Convert.ToInt32(combomaterial.SelectedValue),
                total_price = Convert.ToDecimal(total_price.Text),
                stock_quantity = Convert.ToInt32(stock_quantity.Text),
                is_active = guna2CheckBox1.Checked
            };

            product_action_sql repo = new product_action_sql();
            repo.UpdateProduct(p);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

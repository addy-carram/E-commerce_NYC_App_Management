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

namespace e_commerce_NYC.Products_components
{
    public partial class uc_product_add : Form
    {
        public uc_product_add()
        {
            InitializeComponent();
            LoadMaterials();
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
        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(producer.Text) || string.IsNullOrWhiteSpace(model_name.Text)
                    || string.IsNullOrWhiteSpace(combomaterial.Text) || string.IsNullOrWhiteSpace(total_price.Text)
                    || string.IsNullOrWhiteSpace(stock_quantity.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }
                if(!decimal.TryParse(total_price.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }
                if(!int.TryParse(stock_quantity.Text, out int quantity))
                {
                    MessageBox.Show("Please enter a valid stock quantity.");
                    return;
                }
                
                int materialId = Convert.ToInt32(combomaterial.SelectedValue);
               
                Product p = new Product
                {
                    producer = producer.Text,
                    model_name = model_name.Text,
                    id_type = materialId,
                    total_price = price,
                    stock_quantity = quantity,
                    is_active = true
                };
                product_action_sql repo=new product_action_sql();
                repo.AddProduct(p);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;


            }
        }
    }
}

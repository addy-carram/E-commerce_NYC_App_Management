using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC.Products_components
{
    public partial class uc_product_delete : Form
    {
        public uc_product_delete()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                {
                    MessageBox.Show("Please enter a product ID.");
                    return;
                }
                if(!int.TryParse(guna2TextBox1.Text, out int productId))
                {
                    MessageBox.Show("Please enter a valid numeric product ID.");
                    return;
                }
                product_action_sql repo = new product_action_sql();
                repo.DeleteProduct(productId);
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

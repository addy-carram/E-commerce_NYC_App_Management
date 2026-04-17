using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC.Order_component
{
    public partial class uc_order_edit_id : Form
    {
        public uc_order_edit_id()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                {
                    MessageBox.Show("fill the textbox");
                }
                if(!int.TryParse(guna2TextBox1.Text,out int id))
                {
                    MessageBox.Show("Please enter a valid id.");
                    return;
                }
                uc_order_edit editpage =new uc_order_edit(id);
                if (editpage.ShowDialog() == DialogResult.OK)
                {
                    this.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }
        }
    }
}

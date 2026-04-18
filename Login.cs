using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;


namespace e_commerce_NYC
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
           
            
        
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void userControl11_Click(object sender, EventArgs e)
        {
          
            Form1 dashboard = new Form1();
            dashboard.Show();
            this.FindForm().Hide(); // or this.Close() if you want to close the login form

        }

        private void enter_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string .IsNullOrWhiteSpace(maskedTextBox1.Text)||
                    string.IsNullOrWhiteSpace(guna2ComboBox1.Text))
                {
                    MessageBox.Show("Pleaase fill in all fields.");
                }

                UserSession.Role = guna2ComboBox1.Text;

                Form1 dashboard = new Form1();
                dashboard.Show();
                this.Hide();

            }
            catch( Exception ex)
            {
                MessageBox.Show(" An error occurred:" + ex.Message);
            }
            
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

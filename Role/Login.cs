using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
          
            // or this.Close() if you want to close the login form

        }

        private void enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(maskedTextBox1.Text))
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

                string query = "SELECT password_hash, password_salt, role FROM Users WHERE username=@u";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", guna2TextBox1.Text);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        string savedHash = dr["password_hash"].ToString();
                        string savedSalt = dr["password_salt"].ToString();
                        string savedRole = dr["role"].ToString();

                        byte[] salt = Convert.FromBase64String(savedSalt);

                        string enteredHash = HashHelper.HashPassword(maskedTextBox1.Text, salt);

                        if (enteredHash == savedHash)
                        {
                            MessageBox.Show("Login successful!");

                            
                            Form1 frm = new Form1(savedRole);
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong password!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            if (reg.ShowDialog() == DialogResult.OK)
            {

            }

        }
    }
}

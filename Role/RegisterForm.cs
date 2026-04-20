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

namespace e_commerce_NYC
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            guna2ComboBox1.Items.Add("admin");
            guna2ComboBox1.Items.Add("manager");
            guna2ComboBox1.Items.Add("hr");

            guna2ComboBox1.SelectedIndex = 0;
        }

        private void enter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                    guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

                string query = "INSERT INTO Users (username, password_hash, password_salt, role) VALUES (@u, @p, @s, @r)";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@u", guna2TextBox1.Text);

                    //hash password
                    byte[] salt = HashHelper.GenerateSalt();
                    string hashedPassword = HashHelper.HashPassword(maskedTextBox1.Text, salt);

                    cmd.Parameters.AddWithValue("@p", hashedPassword);
                    cmd.Parameters.AddWithValue("@s", Convert.ToBase64String(salt));

                    // role din comboBox
                    cmd.Parameters.AddWithValue("@r", guna2ComboBox1.SelectedItem.ToString());

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User created successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        }
    }


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

namespace e_commerce_NYC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HideMenuByRole();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel1.Visible=false;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

       

       
        private void label71_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ShowPage(UserControl page)
        {
            panelMain.Controls.Clear();
            page.Dock = DockStyle.Fill;
            panelMain.Controls.Add(page);
        }
        private void panel15_Click(object sender, EventArgs e)
        {
            ShowPage(new UC_Dashboard());
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            ShowPage(new UC_Products());
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            ShowPage(new UC_Patients());
        }

        private void panel18_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            ShowPage(new UC_Orders());
        }

        private void panel16_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            ShowPage(new UC_Employee());
        }

        private void user_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HideMenuByRole()
        {
            if (UserSession.Role == "Employee")
            {
                panel16.Visible = false; 
                panel11.Visible = false; 
            }

            else if (UserSession.Role == "Manager")
            {
                panel16.Visible = false;
                panel11.Visible = false;
            }
            else if(UserSession.Role == "HR")
            {
                panel11.Visible = false;
                panel18.Visible = false;
                panel17.Visible = false;
                panel15.Visible = false;
            }

           
        }
    }
}

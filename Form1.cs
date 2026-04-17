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
            ShowPage(new UC_Dashboard());
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
    }
}

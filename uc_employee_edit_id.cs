using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC
{
    public partial class uc_employee_edit_id : Form
    {
        public uc_employee_edit_id()
        {
            InitializeComponent();
        }

        private void delete_Click(object sender, EventArgs e) //Edit button
        {
            if (!int.TryParse(t_delete.Text, out int id))
            {
                MessageBox.Show("Enter valid ID!");
                return;
            }

            uc_employee_edit form = new uc_employee_edit(id);
            try
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void t_delete_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

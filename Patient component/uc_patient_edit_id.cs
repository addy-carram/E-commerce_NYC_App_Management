using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC.Patient_component
{
    public partial class uc_patient_edit_id : Form
    {
        public uc_patient_edit_id()
        {
            InitializeComponent();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(guna2TextBox1.Text, out int id))
            {
                MessageBox.Show("Enter valid ID!");
                return;
            }

            uc_patient_edit form = new uc_patient_edit(id);
            try
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    
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

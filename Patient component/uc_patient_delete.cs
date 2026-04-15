using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC.Employee_component
{
    public partial class uc_patient_delete : Form
    {
        public uc_patient_delete()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Please enter an employee ID!");
                return;
            }
            if (
        !int.TryParse(guna2TextBox1.Text, out int id))
            {
                MessageBox.Show("Invalid number format!");
                return;
            }
            patient_action_sql repo = new patient_action_sql();
            repo.DeletePerson(id);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

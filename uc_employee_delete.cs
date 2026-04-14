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
    public partial class uc_employee_delete : Form
    {
        public uc_employee_delete()
        {
            InitializeComponent();
        }

        private void t_delete_TextChanged(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(t_delete.Text))
            {
                MessageBox.Show("Please enter an employee ID!");
                return;
            }
            if (
        !int.TryParse(t_delete.Text, out int id))
            {
                MessageBox.Show("Invalid number format!");
                return;
            }
            Employee_Add repo = new Employee_Add();
            repo.DeleteEmployee(id);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

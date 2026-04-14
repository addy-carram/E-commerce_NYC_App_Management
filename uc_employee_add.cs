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
    public partial class uc_employee_add : Form
    {
        public uc_employee_add()
        {
            InitializeComponent();
        }

        private void status_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (
        string.IsNullOrWhiteSpace(t_f_name.Text) ||
        string.IsNullOrWhiteSpace(t_l_name.Text) ||
        string.IsNullOrWhiteSpace(t_phone.Text) ||
        string.IsNullOrWhiteSpace(t_email.Text) ||
        string.IsNullOrWhiteSpace(t_role.Text) ||
        string.IsNullOrWhiteSpace(t_salary.Text))
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }
            if (
        !decimal.TryParse(t_salary.Text, out decimal salary))
            {
                MessageBox.Show("Invalid number format!");
                return;
            }

            Employee newEmployee = new Employee
            {
              
                first_name = t_f_name.Text,
                last_name = t_l_name.Text,
                phone = t_phone.Text,
                email = t_email.Text,
                role = t_role.Text,
                salary = decimal.Parse(t_salary.Text),
                is_active = status.Checked
            };
            Employee_Add repo = new Employee_Add();
            repo.AddPerson(newEmployee);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void t_id_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void uc_employee_add_Load(object sender, EventArgs e)
        {

        }

        private void t_f_name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

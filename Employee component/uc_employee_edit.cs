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
    public partial class uc_employee_edit : Form
    {
        private int employeeId;

        public uc_employee_edit(int id)
        {
            InitializeComponent();

            employeeId = id;

            LoadEmployee();
        }
        private void LoadEmployee()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Employee WHERE id_employee = @id", conn);

                cmd.Parameters.AddWithValue("@id", employeeId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    t_id.Text = reader["id_employee"].ToString();
                    t_id.ReadOnly = true;

                    t_f_name.Text = reader["first_name"].ToString();
                    t_l_name.Text = reader["last_name"].ToString();
                    t_phone.Text = reader["phone"].ToString();
                    t_email.Text = reader["email"].ToString();
                    t_role.Text = reader["id_role"].ToString();
                    t_salary.Text = reader["salary"].ToString();
                    status.Checked = Convert.ToBoolean(reader["is_active"]);
                }
                else
                {
                    MessageBox.Show("Employee not found!");
                    this.Close();
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee
            {
                id = employeeId,
                first_name = t_f_name.Text,
                last_name = t_l_name.Text,
                phone = t_phone.Text,
                email = t_email.Text,
                role = t_role.Text,
                salary = decimal.Parse(t_salary.Text),
                is_active = status.Checked
            };

            Employee_Add repo = new Employee_Add();
            repo.UpdateEmployee(emp);

            MessageBox.Show("Updated!");
            
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
    }


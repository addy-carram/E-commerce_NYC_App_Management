using e_commerce_NYC.Products_components;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC.Order_component
{
    public partial class uc_order_edit : Form
    {
        private int productId;
        string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
        public uc_order_edit(int id)
        {
            InitializeComponent();
            productId = id;
            Loadcombo();
            LoadOrders();
        }

       public void Loadcombo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT id_patient, first_name + ' ' + last_name AS full_name FROM Patient order by first_name"
                    , conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    person_name.DataSource = dt;
                    person_name.DisplayMember = "full_name";
                    person_name.ValueMember = "id_patient";



                    //for employee
                    SqlDataAdapter db = new SqlDataAdapter(
                    "SELECT e.id_employee,e.first_name + ' ' + e.last_name AS full_name FROM Employee e order By e.first_name"
                   , conn);

                    DataTable tb = new DataTable();
                    db.Fill(tb);

                    employee_name.DataSource = tb;
                    employee_name.DisplayMember = "full_name";
                    employee_name.ValueMember = "id_employee";

                    //for product
                    SqlDataAdapter dc = new SqlDataAdapter(
                    "SELECT p.id_product, p.model_name AS full_name FROM Product p", conn);

                    DataTable tc = new DataTable();
                    dc.Fill(tc);

                    produs_name.DataSource = tc;
                    produs_name.DisplayMember = "full_name";
                    produs_name.ValueMember = "id_product";



                    //for order status
                    SqlDataAdapter dd = new SqlDataAdapter(
                    "SELECT os.id_order_status, os.status_name AS full_name FROM Order_Status os", conn);

                    DataTable td = new DataTable();
                    dd.Fill(td);

                    order_status.DataSource = td;
                    order_status.DisplayMember = "full_name";
                    order_status.ValueMember = "id_order_status";


                    //for payment status 
                    SqlDataAdapter de = new SqlDataAdapter(
                     "SELECT os.id_payment_status, os.status_name AS full_name FROM Payment_Status os", conn);

                    DataTable te = new DataTable();
                    de.Fill(te);

                    payment_status.DataSource = te;
                    payment_status.DisplayMember = "full_name";
                    payment_status.ValueMember = "id_payment_status";

                    SqlDataAdapter dg = new SqlDataAdapter(
                     "SELECT os.id_payment_method, os.method_name AS full_name FROM Payment_Method os", conn);

                    DataTable tg = new DataTable();
                    dg.Fill(tg);

                    payment_method.DataSource = tg;
                    payment_method.DisplayMember = "full_name";
                    payment_method.ValueMember = "id_payment_method";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem to acces data from database" + ex.Message);
            }
        }
        private void LoadOrders()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT * FROM Order_Table WHERE id_order = @productId", conn);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id.Text = reader["id_order"].ToString();
                        id.ReadOnly = true;
                        person_name.SelectedValue = reader["id_patient"].ToString();
                        employee_name.SelectedValue = reader["id_employee"].ToString();
                        produs_name.SelectedValue = reader["id_product"].ToString();
                        data.Value = Convert.ToDateTime(reader["order_date"]);
                        price.Text = reader["total_amount"].ToString();
                        payment_method.SelectedValue = reader["id_payment_method"].ToString();
                        payment_status.SelectedValue = reader["id_payment_status"].ToString();
                        order_status.SelectedValue = reader["id_order"].ToString();
                        notes.Text = reader["notes"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Product not found!");
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product: " + ex.Message);
            }
        }
        private void uc_order_edit_Load(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {

                Order o = new Order()
                {
                    id_order = productId,
                    id_patient = Convert.ToInt32(person_name.SelectedValue),
                    id_employee = Convert.ToInt32(employee_name.SelectedValue),
                    id_product = Convert.ToInt32(produs_name.SelectedValue),
                    order_date = data.Value,
                    id_order_status = Convert.ToInt32(order_status.SelectedValue),
                    total_amount = Convert.ToDecimal(price.Text),
                    id_payment_status = Convert.ToInt32(payment_status.SelectedValue),
                    id_payment_method = Convert.ToInt32(payment_method.SelectedValue),
                    notes = notes.Text
                };

                order_action_sql repo = new order_action_sql();
                repo.UpdateOrder(o);
                MessageBox.Show("Order edit successufully");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("error " + ex.Message);
            }
        }

        private void id_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

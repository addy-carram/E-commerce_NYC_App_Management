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

namespace e_commerce_NYC.Order_component
{
    public partial class uc_order_add : Form
    {
        string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
        public uc_order_add()
        {
            InitializeComponent();
            LoadStructureInComboBox();
        }
       private void LoadStructureInComboBox()
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

                    //person_name.DropDownStyle = ComboBoxStyle.DropDown;

                    //person_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //person_name.AutoCompleteSource = AutoCompleteSource.ListItems;


                    //for employee
                    SqlDataAdapter db = new SqlDataAdapter(
                    "SELECT e.id_employee,e.first_name + ' ' + e.last_name AS full_name FROM Employee e order By e.first_name" 
                   , conn);

                    DataTable tb = new DataTable();
                    db.Fill(tb);

                    employee_name.DataSource = tb;
                    employee_name.DisplayMember = "full_name";
                    employee_name.ValueMember = "id_employee";

                    //employee_name.DropDownStyle = ComboBoxStyle.DropDown;

                    //employee_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //employee_name.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //for product
                    SqlDataAdapter dc = new SqlDataAdapter(
                    "SELECT p.id_product, p.model_name AS full_name FROM Product p" , conn);

                    DataTable tc = new DataTable();
                    dc.Fill(tc);

                    produs_name.DataSource = tc;
                    produs_name.DisplayMember = "full_name";
                    produs_name.ValueMember = "id_product";

                    //produs_name.DropDownStyle = ComboBoxStyle.DropDown;
                    //produs_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //produs_name.AutoCompleteSource = AutoCompleteSource.ListItems;


                    //for order status
                    SqlDataAdapter dd = new SqlDataAdapter(
                    "SELECT os.id_order_status, os.status_name AS full_name FROM Order_Status os" , conn);

                    DataTable td = new DataTable();
                    dd.Fill(td);

                    order_status.DataSource = td;
                    order_status.DisplayMember = "full_name";
                    order_status.ValueMember = "id_order_status";

                    //order_status.DropDownStyle = ComboBoxStyle.DropDown;
                    //order_status.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //order_status.AutoCompleteSource = AutoCompleteSource.ListItems;


                    //for payment status 
                    SqlDataAdapter de = new SqlDataAdapter(
                     "SELECT os.id_payment_status, os.status_name AS full_name FROM Payment_Status os" , conn);

                    DataTable te = new DataTable();
                    de.Fill(te);

                    payment_status.DataSource = te;
                    payment_status.DisplayMember = "full_name";
                    payment_status.ValueMember = "id_payment_status";

                    //payment_status.DropDownStyle = ComboBoxStyle.DropDown;
                    //payment_status.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //payment_status.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //for payment method
                    SqlDataAdapter dg = new SqlDataAdapter(
                     "SELECT os.id_payment_method, os.method_name AS full_name FROM Payment_Method os" , conn);

                    DataTable tg = new DataTable();
                    dg.Fill(tg);

                    payment_method.DataSource = tg;
                    payment_method.DisplayMember = "full_name";
                    payment_method.ValueMember = "id_payment_method";

                    //payment_method.DropDownStyle = ComboBoxStyle.DropDown;
                    //payment_method.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    //payment_method.AutoCompleteSource = AutoCompleteSource.ListItems;


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem to acces data from database"+ ex.Message);
            }

        }

        private void payment_method_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(person_name.Text) || string .IsNullOrWhiteSpace(produs_name.Text) || string.IsNullOrWhiteSpace(employee_name.Text)
                    || string.IsNullOrWhiteSpace(price.Text) || string.IsNullOrWhiteSpace(data.Text) || string.IsNullOrWhiteSpace(order_status.Text) 
                    || string.IsNullOrWhiteSpace(payment_status.Text) || string.IsNullOrWhiteSpace(payment_method.Text) || string.IsNullOrWhiteSpace(notes.Text)
                    )
                {
                    MessageBox.Show("Please fill all fields.");
                }
                if(!decimal.TryParse(price.Text, out decimal total_amount))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                ///here we will convert all from combobox data
                ///
                Order o = new Order()
                {
                    id_patient = Convert.ToInt32(person_name.SelectedValue),
                    id_product = Convert.ToInt32(produs_name.SelectedValue),
                    id_employee = Convert.ToInt32(employee_name.SelectedValue),
                    total_amount = total_amount,
                    order_date = Convert.ToDateTime(data.Text),
                    id_order_status = Convert.ToInt32(order_status.SelectedValue),
                    id_payment_status = Convert.ToInt32(payment_status.SelectedValue),
                    id_payment_method = Convert.ToInt32(payment_method.SelectedValue),
                    notes = notes.Text
                };
                order_action_sql repo = new order_action_sql();
                repo.AddOrder(o);
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }

        private void order_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

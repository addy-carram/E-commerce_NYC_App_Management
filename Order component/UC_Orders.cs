using e_commerce_NYC.Order_component;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC
{
    public partial class UC_Orders : UserControl
    {
        private DataTable fullData, fullData2,fullData3,fullData4;
        private int pageSize = 10;
        private int currentPage = 0;
        public UC_Orders()
        {
            InitializeComponent();
            LoadOrders();
            LoadOrderProcessing();
            LoadOrderRefund();
        }
        private void LoadOrders()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                        "er.role_name, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
                        " os.status_name AS status,   ps.status_name AS plata,pm.method_name AS metoda" +
                        " FROM Order_Table o" +
                        " JOIN Employee e ON o.id_employee=e.id_employee" +
                        " JOIN Employee_Role er ON e.id_role=er.id_role " +
                        " JOIN Patient p ON o.id_patient = p.id_patient " +
                        " JOIN Product prod ON o.id_product = prod.id_product " +
                        " JOIN Product_Type pt ON prod.id_product_type = pt.id_product_type" +
                        " JOIN Order_Status os ON o.id_order_status = os.id_order_status " +
                        " JOIN Payment_Status ps ON o.id_payment_status = ps.id_payment_status" +
                        " JOIN Payment_Method pm ON o.id_payment_method=pm.id_payment_method" +
                        " ORDER BY o.id_order DESC;", conn);

                    fullData = new DataTable();
                    adapter.Fill(fullData);
                    ShowPage(0,panelPagination,guna2DataGridView1,fullData);
                    guna2DataGridView1.DataSource = fullData;
                }
                if (!guna2DataGridView1.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn deleteBtn =
                        new DataGridViewButtonColumn();
                    DataGridViewButtonColumn editBtn =
                        new DataGridViewButtonColumn();
                    deleteBtn.Name = "btnDelete";
                    deleteBtn.HeaderText = "";
                    deleteBtn.Text = "Delete";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;

                    editBtn.Name = "btnEdit";
                    editBtn.HeaderText = "";
                    editBtn.Text = "Edit";
                    editBtn.UseColumnTextForButtonValue = true;
                    editBtn.Width = 80;

                    guna2DataGridView1.Columns.Add(deleteBtn);
                    guna2DataGridView1.Columns.Add(editBtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }
        private void LoadOrderProcessing()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                        "er.role_name, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
                        " os.status_name AS status,   ps.status_name AS plata,pm.method_name AS metoda" +
                        " FROM Order_Table o" +
                        " JOIN Employee e ON o.id_employee=e.id_employee" +
                        " JOIN Employee_Role er ON e.id_role=er.id_role " +
                        " JOIN Patient p ON o.id_patient = p.id_patient " +
                        " JOIN Product prod ON o.id_product = prod.id_product " +
                        " JOIN Product_Type pt ON prod.id_product_type = pt.id_product_type" +
                        " JOIN Order_Status os ON o.id_order_status = os.id_order_status " +
                        " JOIN Payment_Status ps ON o.id_payment_status = ps.id_payment_status" +
                        " JOIN Payment_Method pm ON o.id_payment_method=pm.id_payment_method" +
                        " where os.status_name='processing' or os.status_name='pending'", conn);

                    fullData3 = new DataTable();
                    adapter.Fill(fullData3);
                    ShowPage(0,panelPagination1,guna2DataGridView3,fullData3);
                    guna2DataGridView3.DataSource = fullData3;
                }
                if (!guna2DataGridView3.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn deleteBtn =
                        new DataGridViewButtonColumn();
                    DataGridViewButtonColumn editBtn =
                        new DataGridViewButtonColumn();
                    deleteBtn.Name = "btnDelete";
                    deleteBtn.HeaderText = "";
                    deleteBtn.Text = "Delete";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;

                    editBtn.Name = "btnEdit";
                    editBtn.HeaderText = "";
                    editBtn.Text = "Edit";
                    editBtn.UseColumnTextForButtonValue = true;
                    editBtn.Width = 80;

                    guna2DataGridView4.Columns.Add(deleteBtn);
                    guna2DataGridView4.Columns.Add(editBtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }
        private void LoadOrderRefund()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                        "er.role_name, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
                        " os.status_name AS status,   ps.status_name AS plata,pm.method_name AS metoda" +
                        " FROM Order_Table o" +
                        " JOIN Employee e ON o.id_employee=e.id_employee" +
                        " JOIN Employee_Role er ON e.id_role=er.id_role " +
                        " JOIN Patient p ON o.id_patient = p.id_patient " +
                        " JOIN Product prod ON o.id_product = prod.id_product " +
                        " JOIN Product_Type pt ON prod.id_product_type = pt.id_product_type" +
                        " JOIN Order_Status os ON o.id_order_status = os.id_order_status " +
                        " JOIN Payment_Status ps ON o.id_payment_status = ps.id_payment_status" +
                        " JOIN Payment_Method pm ON o.id_payment_method=pm.id_payment_method" +
                        " where ps.status_name='refund'", conn);

                    fullData4 = new DataTable();
                    adapter.Fill(fullData4);
                    ShowPage(0,panelPagination2,guna2DataGridView4,fullData4);
                    guna2DataGridView4.DataSource = fullData4;
                }
                if (!guna2DataGridView4.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn deleteBtn =
                        new DataGridViewButtonColumn();
                    DataGridViewButtonColumn editBtn =
                        new DataGridViewButtonColumn();
                    deleteBtn.Name = "btnDelete";
                    deleteBtn.HeaderText = "";
                    deleteBtn.Text = "Delete";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;

                    editBtn.Name = "btnEdit";
                    editBtn.HeaderText = "";
                    editBtn.Text = "Edit";
                    editBtn.UseColumnTextForButtonValue = true;
                    editBtn.Width = 80;

                    guna2DataGridView4.Columns.Add(deleteBtn);
                    guna2DataGridView4.Columns.Add(editBtn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                uc_order_add AddOrder= new uc_order_add();
                if (AddOrder.ShowDialog() == DialogResult.OK)
                {
                    LoadOrders();
                    LoadOrderProcessing();
                    LoadOrderRefund();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ShowPage(int page, Panel panelPagination,DataGridView guna2DataGridView1,DataTable fullData)
        {
            currentPage = page;
            int totalPages = (int)Math.Ceiling((double)fullData.Rows.Count / pageSize);

            // Taie datele pentru pagina curentă
            var pageData = fullData.AsEnumerable()
                .Skip(page * pageSize)
                .Take(pageSize)
                .CopyToDataTable();

            guna2DataGridView1.DataSource = pageData;
            guna2DataGridView1.ScrollBars = ScrollBars.None;

            // Ajustează înălțimea automată
            int height = guna2DataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                height += row.Height;
            guna2DataGridView1.Height = height;

            // Actualizează butoanele de paginare
            UpdatePaginationButtons(totalPages, panelPagination, guna2DataGridView1,fullData);
        }


        private void UpdatePaginationButtons(int totalPages, Panel panelPagination, DataGridView guna2DataGridView1,DataTable fullData)
        {
            panelPagination.Controls.Clear();
            int btnWidth = 35;
            int x = 0;

            for (int i = 0; i < totalPages; i++)
            {
                int pageIndex = i;
                Button btn = new Button();
                btn.Text = (i + 1).ToString();
                btn.Width = btnWidth;
                btn.Height = 35;
                btn.Left = x;
                btn.FlatStyle = FlatStyle.Flat;

                if (i == currentPage)
                {
                    btn.BackColor = Color.FromArgb(66, 133, 244); // albastru ca Google
                    btn.ForeColor = Color.White;
                }
                else
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.FromArgb(66, 133, 244);
                }

                btn.Click += (s, e) => ShowPage(pageIndex, panelPagination, guna2DataGridView1, fullData);
                panelPagination.Controls.Add(btn);
                x += btnWidth + 5;
            }
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

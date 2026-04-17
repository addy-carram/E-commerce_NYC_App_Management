using e_commerce_NYC.Order_component;
using e_commerce_NYC.Products_components;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace e_commerce_NYC
{
    public partial class UC_Orders : UserControl
    {
        private DataTable fullData, fullData2, fullData3, fullData4, fullData5;
        private int pageSize = 10;
        private int currentPage = 0;
        public UC_Orders()
        {
            InitializeComponent();
            LoadOrders();
            LoadTable2();
            LoadOrderProcessing();
            LoadOrderRefund();
            LoadOrdersCancelled();
            LoadAnalytics();
            LoadPaymentMethodChart();
            LoadOrdersByYearChart();
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
                        "er.role_name AS role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                        " WHERE os.status_name <> 'cancelled'" +
                        " ORDER BY o.id_order DESC;", conn);

                    fullData = new DataTable();
                    adapter.Fill(fullData);
                    ShowPage(0, panelPagination, guna2DataGridView1, fullData);
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
                    deleteBtn.Text = "Cancel";
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
        private void LoadOrdersCancelled()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                        "er.role_name AS role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                        " WHERE os.status_name = 'cancelled'" +
                        " ORDER BY o.id_order ;", conn);

                    fullData5= new DataTable();
                    adapter.Fill(fullData5);
                    guna2DataGridView5.DataSource = fullData5;
                }
                if (!guna2DataGridView5.Columns.Contains("btnDelete"))
                {
                    DataGridViewButtonColumn deleteBtn =
                        new DataGridViewButtonColumn();
                    DataGridViewButtonColumn editBtn =
                        new DataGridViewButtonColumn();
                    deleteBtn.Name = "btnDelete";
                    deleteBtn.HeaderText = "";
                    deleteBtn.Text = "Uncancelled";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;
                    guna2DataGridView5.Columns.Add(deleteBtn);
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
                    ShowPage(0, panelPagination1, guna2DataGridView3, fullData3);
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
                    deleteBtn.Text = "Contact";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;


                    guna2DataGridView3.Columns.Add(deleteBtn);
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
                        "er.role_name as role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                    ShowPage(0, panelPagination2, guna2DataGridView4, fullData4);
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
                    deleteBtn.Text = "Contact";
                    deleteBtn.UseColumnTextForButtonValue = true;
                    deleteBtn.Width = 80;

                    guna2DataGridView4.Columns.Add(deleteBtn);
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
                uc_order_add AddOrder = new uc_order_add();
                if (AddOrder.ShowDialog() == DialogResult.OK)
                {
                    LoadOrders();
                    LoadOrderProcessing();
                    LoadOrderRefund();
                    MessageBox.Show("Order successufully added");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ShowPage(int page, Panel panelPagination, DataGridView guna2DataGridView1, DataTable fullData)
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
            UpdatePaginationButtons(totalPages, panelPagination, guna2DataGridView1, fullData);
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                switch (guna2ComboBox1.SelectedItem.ToString())
                {
                    case "Customer":
                        {
                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(
                                    @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                                    "er.role_name as role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                                    " WHERE os.status_name <> 'cancelled'" +
                                    " Order by client", conn);

                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    case "Employee":
                        {
                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(
                                    @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                                    "er.role_name as role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                                    " WHERE os.status_name <> 'cancelled'" +
                                    " Order by employee", conn);

                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    case "Payment status":
                        {
                            using (SqlConnection conn = new SqlConnection(connStr))
                            {
                                conn.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(
                                    @"SELECT   o.id_order,  p.first_name + ' ' + p.last_name AS client,e.first_name+' '+e.last_name AS employee," +
                                    "er.role_name as role, prod.model_name AS produs,  pt.type_name AS tip_produs,  o.total_amount AS pret,  " +
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
                                    " WHERE os.status_name <> 'cancelled'" +
                                    " Order by plata", conn);

                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    default:
                        {
                            LoadOrders();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void UpdatePaginationButtons(int totalPages, Panel panelPagination, DataGridView guna2DataGridView1, DataTable fullData)
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
        }//textboxul for the searching
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                LoadOrders();
                return;
            }

            SearchProducts(guna2TextBox1.Text);
        }
        private void SearchProducts(string text)
        {
            text = text.Replace("'", "''");

            DataView dv = fullData.DefaultView;

            dv.RowFilter =
                $"client LIKE '%{text}%' OR produs LIKE '%{text}%' OR employee LIKE '%{text}%'" +
                $" OR role LIKE '%{text}%' OR produs LIKE '%{text}%'";

            guna2DataGridView1.DataSource = dv;
            if (dv == null)
                MessageBox.Show("No results found!");
        }
        //the kpi panel of general informationabout the orders
        private void LoadAnalytics()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Order_Table", conn);
                    int totalOrders = Convert.ToInt32(cmd.ExecuteScalar());
                    SqlCommand cmd1 = new SqlCommand("SELECT SUM(total_amount) FROM Order_Table", conn);
                    decimal totalRevenue = Convert.ToDecimal(cmd1.ExecuteScalar());
                    SqlCommand cmd2 = new SqlCommand("SELECT (SUM(CASE WHEN id_payment_status = 5 THEN 1 ELSE 0 END)*100)/Count(id_order) FROM Order_Table", conn);
                    decimal percentage = Convert.ToDecimal(cmd2.ExecuteScalar());
                    SqlCommand cmd3 = new SqlCommand("SELECT TOP 1 pt.type_name FROM Order_Table o JOIN Product p On o.id_product=p.id_product\r\nJOIN Product_Type pt On p.id_product_type=pt.id_product_type", conn);
                    string topproducts = cmd3.ExecuteScalar().ToString();

                    label3.Text =totalOrders.ToString();
                    label4.Text = totalRevenue.ToString();
                    label5.Text = percentage.ToString() + "%";
                    label6.Text = topproducts;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                uc_order_edit_id editpage = new uc_order_edit_id();
                if(editpage.ShowDialog() != DialogResult.OK)
                {
                    LoadOrders();
                    LoadOrderProcessing();
                    LoadOrderRefund();
                }




            } catch(Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            
            try
            {
                uc_order_delete deletepage = new uc_order_delete();
                if(deletepage.ShowDialog() == DialogResult.OK)
                {
                    LoadOrders();
                    LoadOrderProcessing();
                    LoadOrderRefund();
                    MessageBox.Show("Order successufully deleted");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }

        }

        private void guna2DataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (guna2DataGridView5.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    int id = Convert.ToInt32(guna2DataGridView5.Rows[e.RowIndex].Cells["id_order"].Value);
                    try
                    {
                        DialogResult confirm = MessageBox.Show(
                        "Are you sure you want to uncancel?",
                        "Confirm",
                        MessageBoxButtons.YesNo);

                        if (confirm == DialogResult.Yes)
                        {

                            order_action_sql repo = new order_action_sql();
                            repo.UNCancelOrder(id);
                            LoadOrders();
                            LoadOrdersCancelled();
                            MessageBox.Show("Order canceled successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (guna2DataGridView3.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    int id = Convert.ToInt32(guna2DataGridView3.Rows[e.RowIndex].Cells["id_order"].Value);
                    try
                    {
                        DialogResult confirm = MessageBox.Show(
                        "Are you sure you want to call?",
                        "Confirm",
                        MessageBoxButtons.YesNo);

                        if (confirm == DialogResult.Yes)
                        {

                            uc_order_edit epp = new uc_order_edit(id);
                            epp.SetButtonText("Call");

                            if (epp.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show("Order canceled successfully!");
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void guna2DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (guna2DataGridView4.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    int id = Convert.ToInt32(guna2DataGridView4.Rows[e.RowIndex].Cells["id_order"].Value);
                    try
                    {
                        DialogResult confirm = MessageBox.Show(
                        "Are you sure you want to call?",
                        "Confirm",
                        MessageBoxButtons.YesNo);

                        if (confirm == DialogResult.Yes)
                        {

                            uc_order_edit epp = new uc_order_edit(id);
                            epp.SetButtonText("Call");

                            if (epp.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show("Order canceled successfully!");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnDelete")
                    {
                        int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_order"].Value);
                        try
                        {
                            DialogResult confirm = MessageBox.Show(
                            "Are you sure you want to cancel?",
                            "Confirm",
                            MessageBoxButtons.YesNo);

                            if (confirm == DialogResult.Yes)
                            {

                                order_action_sql repo = new order_action_sql();
                                repo.CancelOrder(id);
                                LoadOrders();
                                LoadOrdersCancelled();
                                MessageBox.Show("Order canceled successfully!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                    else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
                    {
                        try
                        {
                            DialogResult confirm = MessageBox.Show(
                            "Are you sure you want to edit?",
                            "Confirm",
                            MessageBoxButtons.YesNo);

                            if (confirm == DialogResult.Yes)
                            {

                                int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_order"].Value);
                                uc_order_edit editForm = new uc_order_edit(id);

                                if (editForm.ShowDialog() == DialogResult.OK)
                                {

                                    LoadOrders();
                                    MessageBox.Show("Order edited successfully!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// method of payment of our client 
        /// </summary>
        private void LoadPaymentMethodChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
                pm.method_name,
                COUNT(o.id_order) AS TotalOrders
            FROM Order_Table o
            JOIN Payment_Method pm 
                ON o.id_payment_method = pm.id_payment_method
            GROUP BY pm.method_name
            ORDER BY TotalOrders DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                chart1.Series.Clear();
                chart1.Titles.Clear();

                chart1.Titles.Add("Payment Methods");

                Series series = new Series("Methods");
                series.ChartType = SeriesChartType.Pie;   // or Column

                chart1.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(
                        row["method_name"].ToString(),
                        row["TotalOrders"]);
                }

                series.IsValueShownAsLabel = true;
                chart1.Legends[0].Enabled = true;
            }
        }
        /// <summary>
        /// one char about our number of orders over 3 years
        /// </summary>
        private void LoadOrdersByYearChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
                YEAR(order_date) AS An,
                COUNT(id_order) AS TotalOrders
            FROM Order_Table
            WHERE YEAR(order_date) IN (2023, 2024, 2025)
            GROUP BY YEAR(order_date)
            ORDER BY An";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                chart2.Series.Clear();
                chart2.Titles.Clear();

                chart2.Titles.Add("Orders by Year");

                Series series = new Series("Orders");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.IsValueShownAsLabel = true;
                chart2.Series.Add(series);
                series.ChartType = SeriesChartType.Line;

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(
                        row["An"].ToString(),
                        row["TotalOrders"]);
                }

                series.IsValueShownAsLabel = true;
            }
        }
        private void LoadTable2()
        {
            try
            {
                string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Select * From HR.vw_Vanzari_Pe_Categorii", conn);
                    fullData2 = new DataTable();
                    adapter.Fill(fullData2);
                    guna2DataGridView2.DataSource = fullData2;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }
    }
}

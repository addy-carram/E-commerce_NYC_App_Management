using e_commerce_NYC.Products_components;
using System;
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
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace e_commerce_NYC
{
    public partial class UC_Products : UserControl
    {
        private DataTable fullData, fullData2;
        private int pageSize = 10; 
        private int currentPage = 0;
        public UC_Products()
        {
            InitializeComponent();
            LoadProducts();
            LoadAnalytics();
            LoadTable();
            LoadTop5ProducersChart();
            LoadTop5MaterialsChart();
        }
        private void LoadProducts()
        {
            string connSt= @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security = True; TrustServerCertificate = True";
            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();
                string query = "Select id_product,model_name,type_name,producer,stock_quantity,total_price,Venit,TVA,Pret_vanzari " +
                    "From Product P JOIN Product_Type Pt ON P.id_product_type=Pt.id_product_type ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                fullData = new DataTable();
                adapter.Fill(fullData);
                ShowPage(0);
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
        private void ShowPage(int page)
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
            UpdatePaginationButtons(totalPages);
        }
        //pagination the table of products
        private void UpdatePaginationButtons(int totalPages)
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

                btn.Click += (s, e) => ShowPage(pageIndex);
                panelPagination.Controls.Add(btn);
                x += btnWidth + 5;
            }
        }
        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                LoadProducts();
                return;
            }

            SearchProducts(guna2TextBox1.Text);
        }

        private void add_Click(object sender, EventArgs e)
        {
            try
            {
                uc_product_add addForm = new uc_product_add();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                    MessageBox.Show("Product added successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
                
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                uc_product_delete deleteForm = new uc_product_delete();
                if (deleteForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                    MessageBox.Show("Product deleted successfully!");
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
                uc_product_edit_id editForm = new uc_product_edit_id();
                if (editForm.ShowDialog() != DialogResult.OK)
                {
                    LoadProducts();
                    MessageBox.Show("Product edited successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
                        int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_product"].Value);
                        try
                        {
                            DialogResult confirm = MessageBox.Show(
                            "Are you sure you want to delete?",
                            "Confirm",
                            MessageBoxButtons.YesNo);

                            if (confirm == DialogResult.Yes)
                            {

                                product_action_sql repo = new product_action_sql();
                                repo.DeleteProduct(id);
                                LoadProducts();
                                MessageBox.Show("Product deleted successfully!");
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

                                int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_product"].Value);
                                uc_product_edit editForm = new uc_product_edit(id);

                                if (editForm.ShowDialog() == DialogResult.OK)
                                {

                                    LoadProducts();
                                    MessageBox.Show("Product edited successfully!");
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

        private void SearchProducts(string text)
        {
            text = text.Replace("'", "''");

            DataView dv = fullData.DefaultView;

            dv.RowFilter =
                $"model_name LIKE '%{text}%' OR producer LIKE '%{text}%' OR type_name LIKE '%{text}%'";

            guna2DataGridView1.DataSource = dv;
            if (dv == null)
                MessageBox.Show("No results found!");
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string connSt = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";
                switch (guna2ComboBox1.SelectedItem.ToString())
                {
                    case "Price":
                        {
                            guna2DataGridView1.AutoGenerateColumns = false;
                            string query = "Select id_product,model_name,type_name,producer,stock_quantity,total_price,Venit,TVA,Pret_vanzari " + "From Product P JOIN Product_Type Pt ON P.id_product_type=Pt.id_product_type ORDER BY total_price";
                            using (SqlConnection conn = new SqlConnection(connSt))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    case "Producer":
                        {
                            guna2DataGridView1.AutoGenerateColumns = false;
                            string query = "Select id_product,model_name,type_name,producer,stock_quantity,total_price,Venit,TVA,Pret_vanzari " +"From Product P JOIN Product_Type Pt ON P.id_product_type=Pt.id_product_type ORDER BY producer";
                            using (SqlConnection conn = new SqlConnection(connSt))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    case "Type":
                        {
                            guna2DataGridView1.AutoGenerateColumns = false;
                            string query = "Select id_product,model_name,type_name,producer,stock_quantity,total_price,Venit,TVA,Pret_vanzari " +"From Product P JOIN Product_Type Pt ON P.id_product_type=Pt.id_product_type ORDER BY type_name";
                            using (SqlConnection conn = new SqlConnection(connSt))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                guna2DataGridView1.DataSource = dt;
                            }
                            break;
                        }
                    default:
                        {
                            LoadProducts();
                            break;
                        }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadAnalytics()
        {
            try
            {
                string connSt = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connSt))
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT count(id_product) FROM Product", conn);
                    int totalProducts = (int)cmd1.ExecuteScalar();
                    SqlCommand cmd2 = new SqlCommand("SELECT SUM(total_price*stock_quantity) FROM Product", conn);
                    decimal totalRevenue = (decimal)cmd2.ExecuteScalar();
                    SqlCommand cmd3 = new SqlCommand("SELECT TOP 1 producer FROM Product Order By total_price DESC", conn);
                    string topProducer = (string)cmd3.ExecuteScalar();
                    SqlCommand cmd4 = new SqlCommand("Select TOP 1 total_price FROM Product ", conn);
                    decimal highestPrice = (decimal)cmd4.ExecuteScalar();
                    label3.Text = totalProducts.ToString();
                    label4.Text = totalRevenue.ToString();
                    label5.Text = topProducer;
                    label6.Text = highestPrice.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }
        public void LoadTable()
        {
            string connSt=@"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();
                string query = "SELECT TOP 3 model_name,SUM(total_price *stock_quantity) as total From Product group by model_name ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                fullData2 = new DataTable();
                adapter.Fill(fullData2);
                guna2DataGridView2.DataSource = fullData2;
            }
        }
        private void LoadTop5ProducersChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            string query = @"
        SELECT TOP 5
            producer,
            SUM(Pret_vanzari) AS TotalVenit
        FROM Product P
        JOIN Product_Type Pt ON P.id_product_type = Pt.id_product_type
        GROUP BY producer
        ORDER BY TotalVenit DESC";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                chart1.Series.Clear();
                chart1.Titles.Clear();

                chart1.Titles.Add("Top 5 Producers - Venit");

                Series series = new Series("Venit");
                series.ChartType = SeriesChartType.Column;

                chart1.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["producer"].ToString(), row["TotalVenit"]);
                }
            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadTop5MaterialsChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            string query = @"
        SELECT TOP 5
            type_name,
            SUM(Venit) AS TotalVenit
        FROM Product P
        JOIN Product_Type Pt ON P.id_product_type = Pt.id_product_type
        GROUP BY type_name
        ORDER BY TotalVenit DESC";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                chart2.Series.Clear();
                chart2.Titles.Clear();

                chart2.Titles.Add("Top 5 Materials - Venit");

                Series series = new Series("Venit");
                series.ChartType = SeriesChartType.Column;

                chart2.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["type_name"].ToString(), row["TotalVenit"]);
                }
            }
        }

    }
}

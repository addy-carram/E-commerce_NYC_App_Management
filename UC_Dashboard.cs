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
using System.Windows.Forms.DataVisualization.Charting;

namespace e_commerce_NYC
{
    public partial class UC_Dashboard : UserControl
    {
        string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security = True; TrustServerCertificate = True";

        public UC_Dashboard()
        {
            InitializeComponent();
            LoadAnalitics();
            LoadChar1();
            LoadChar2();
            LoadTable2();
            LoadChar4();
            LoadChar5();
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadAnalitics()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connSt))
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("Select Sum(total_amount) From Order_Table", conn);
                    decimal revenue = Convert.ToDecimal(cmd1.ExecuteScalar());
                    SqlCommand cmd2 = new SqlCommand("Select Sum(Pret_vanzari) From Product", conn);
                    decimal costProduse = Convert.ToDecimal(cmd2.ExecuteScalar());
                    SqlCommand cmd3 = new SqlCommand("Select Sum(o.total_amount)/Sum(p.Pret_vanzari) From Product p JOIN Order_Table o On o.id_product=p.id_product", conn);
                    decimal percentage = Convert.ToDecimal(cmd3.ExecuteScalar());
                    SqlCommand cmd4 = new SqlCommand("SELECT (SUM(CASE WHEN id_payment_status = 5 THEN 1 ELSE 0 END)*100)/Count(id_order) FROM Order_Table", conn);
                    decimal percentageCustomer = Convert.ToDecimal(cmd4.ExecuteScalar());
                    SqlCommand cmd5 = new SqlCommand("Select Count(*) From Employee", conn);
                    int totalEmployee = Convert.ToInt32(cmd5.ExecuteScalar());
                    label1.Text = revenue.ToString()+"$";
                    label3.Text = costProduse.ToString()+"$";
                    label5.Text = percentage.ToString()+"%";
                    label7.Text = percentageCustomer.ToString()+"%";
                    label9.Text = totalEmployee.ToString();

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void LoadChar1()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
            (SELECT SUM(total_amount) FROM Order_Table) AS Revenue,
            (SELECT SUM(Pret_vanzari) FROM Product) AS Cost";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                chart1.Series.Clear();
                chart1.Titles.Clear();

                chart1.Titles.Add("Profit");

                Series series = new Series("Profit");
                series.ChartType = SeriesChartType.Bar;   // or Column

                chart1.Series.Add(series);

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(
                        "Revenue",
                        row["Revenue"]);
                    series.Points.AddXY(
                        "Cost",
                        row["Cost"]);
                }

                series.IsValueShownAsLabel = true;
                chart1.Legends[0].Enabled = true;
            }

        }
        private void LoadChar2()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                            SELECT 
                orr.status_name,
                COUNT(*) AS Total
                FROM Order_Table o
                JOIN Order_Status orr ON o.id_order_status=orr.id_order_status
                GROUP BY orr.status_name";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                chart2.Series.Clear();
                chart2.Titles.Clear();

                chart2.Titles.Add("Order Status Distribution");

                Series series = new Series("Orders");
                series.ChartType = SeriesChartType.Pie;
                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(
                        row["status_name"].ToString(),
                        row["Total"]);
                }

                series.IsValueShownAsLabel = true;
                chart2.Series.Add(series);
                chart2.Legends[0].Enabled = true;
            }
        }
        private void LoadTable2()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                                SELECT 
                    YEAR(order_date) AS OrderYear,
                    SUM(total_amount) AS Revenue
                FROM Order_Table
                GROUP BY YEAR(order_date)
                ORDER BY OrderYear";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;
            }
        }
        private void LoadChar4()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                            SELECT TOP 5
                p.model_name,
                COUNT(o.id_product) AS TotalSold
                FROM Order_Table o
                JOIN Product p ON p.id_product = o.id_product
                GROUP BY p.model_name
                ORDER BY TotalSold DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                chart4.Series.Clear();
                chart4.Titles.Clear();

                chart4.Titles.Add("Top Products");

                Series series = new Series("Products");
                series.ChartType = SeriesChartType.Bar;



                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(
                        row["model_name"],
                        row["TotalSold"]);
                }

                series.IsValueShownAsLabel = true;
                chart4.Series.Add(series);
                chart4.Legends[0].Enabled = true;
            }
        }
        private void LoadChar5()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
            SUM(CASE WHEN id_payment_status = 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) 
            FROM Order_Table";

                SqlCommand cmd = new SqlCommand(query, conn);
                decimal rate = Convert.ToDecimal(cmd.ExecuteScalar());

                chart5.Series.Clear();
                chart5.Titles.Clear();

                chart5.Titles.Add("Payment Success Rate");

                Series series = new Series("Success");
                series.ChartType = SeriesChartType.Doughnut;

                series.Points.AddXY("Success", rate);
                series.Points.AddXY("Remaining", 100 - rate);

                series.IsValueShownAsLabel = true;

                chart5.Series.Add(series);
                chart5.Legends[0].Enabled = true;
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

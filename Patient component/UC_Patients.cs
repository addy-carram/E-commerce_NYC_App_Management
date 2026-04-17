using e_commerce_NYC.Employee_component;
using e_commerce_NYC.Patient_component;
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

namespace e_commerce_NYC
{
    public partial class UC_Patients : UserControl
    {
        private DataTable fullData,fullData2;
        private int pageSize = 10; 
        private int currentPage = 0;
        public UC_Patients()
        {
            InitializeComponent();
            LoadPatients();
            LoadAgeChart();
            LoadCityChart();
            LoadTopOrder();
            LoadAnalist();
        }
  public void LoadPatients()
        {
            string connSr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            using(SqlConnection conn = new SqlConnection(connSr))
            {
                conn.Open();
                string query = "Select * From Patient";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                fullData=new DataTable();
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
        private void LoadTopOrder()
        {
            string connST= @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connST))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 5 first_name, last_name,type_name,model_name,total_amount FROM\r\nOrder_Table O\r\nJOIN Patient P ON O.id_patient=P.id_patient\r\nJOIN Product Pr ON O.id_product=Pr.id_product\r\nJOIN Product_Type PT ON Pr.id_product_type=PT.id_product_type\r\nORDER BY total_amount desc", conn);
                   SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    fullData2 = new DataTable();
                    adapter.Fill(fullData2);
                    ShowPage(0);
                    guna2DataGridView2.DataSource = fullData2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error at patient table: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        

        private void EditPatient(int id)
        {
            string connStr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Patient SET ... WHERE id_patient = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error at patient table: " + ex.Message);
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
        private void panelPagination_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                LoadPatients();
                return;
            }

            SearchPatients(guna2TextBox1.Text);
        }
        private void SearchPatients(string text)
        {
            text = text.Replace("'", "''");

            DataView dv = fullData.DefaultView;

            dv.RowFilter =
                $"first_name LIKE '%{text}%' OR last_name LIKE '%{text}%'";

            guna2DataGridView1.DataSource = dv;
            if (dv == null)
                MessageBox.Show("No results found!");
        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void add_Click(object sender, EventArgs e)
        {

        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == guna2DataGridView1.Columns["btnDelete"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_patient"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete?",
                    "Confirm",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    patient_action_sql form = new patient_action_sql();
                    form.DeletePerson(id);
                    LoadPatients(); // reîncarcă datele
                }
            }
            if (e.ColumnIndex == guna2DataGridView1.Columns["btnEdit"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["id_patient"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to edit?",
                    "Confirm",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    uc_patient_edit form = new uc_patient_edit(id);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadPatients();
                    } // refresh datele
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (guna2ComboBox1.SelectedItem.ToString())
            {
                case "Date":
                    {
                        string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
                        using (SqlConnection conn = new SqlConnection(connSt))
                        {
                            conn.Open();
                            string query = "Select * From Patient Order by date_of_birth";
                            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                            fullData = new DataTable();
                            adapter.Fill(fullData);
                            guna2DataGridView1.DataSource = fullData;
                        }

                        break;
                    }

                case "Country":
                    {
                        string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
                        using (SqlConnection conn = new SqlConnection(connSt))
                        {
                            conn.Open();
                            string query = "Select * From Patient Order by country";
                            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                            fullData = new DataTable();
                            adapter.Fill(fullData);
                            guna2DataGridView1.DataSource = fullData;
                        }

                        break;
                    }
                case "City":
                    {
                        string connSt = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
                        using (SqlConnection conn = new SqlConnection(connSt))
                        {
                            conn.Open();
                            string query = "Select * From Patient Order by city";
                            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                            fullData = new DataTable();
                            adapter.Fill(fullData);
                            guna2DataGridView1.DataSource = fullData;
                        }

                        break;
                    }
                default:
                    LoadPatients();
                    break;
            }
        }

        private void add_Click_1(object sender, EventArgs e)
        {
            uc_patient_add uc_Patient_Add = new uc_patient_add();
            if(uc_Patient_Add.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
                MessageBox.Show("Patient added successfully!");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            uc_patient_delete form = new uc_patient_delete();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
                MessageBox.Show("Employee deleted successfully!");
            }
        }

        private void edit_Click_1(object sender, EventArgs e)
        {
            uc_patient_edit_id form= new uc_patient_edit_id();
            if(form.ShowDialog() != DialogResult.OK)
            {
                LoadPatients();
                MessageBox.Show("Patient edited successfully!");
            }
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        public void LoadAgeChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
        SELECT 
            CASE 
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 0 AND 18 THEN '0-18'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 19 AND 30 THEN '19-30'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 31 AND 45 THEN '31-45'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 46 AND 60 THEN '46-60'
                ELSE '60+'
            END AS age_group,
            COUNT(*) AS total
        FROM Patient
        GROUP BY 
            CASE 
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 0 AND 18 THEN '0-18'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 19 AND 30 THEN '19-30'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 31 AND 45 THEN '31-45'
                WHEN DATEDIFF(YEAR, date_of_birth, GETDATE()) BETWEEN 46 AND 60 THEN '46-60'
                ELSE '60+'
            END";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Clear chart
            chart1.Series.Clear();
            chart1.Titles.Clear();

            chart1.Titles.Add("Patients Age Distribution");

            Series series = new Series("Ages");
            series.ChartType = SeriesChartType.Column; // you can change to Pie

            foreach (DataRow row in dt.Rows)
            {
                string ageGroup = row["age_group"].ToString();
                int total = Convert.ToInt32(row["total"]);

                series.Points.AddXY(ageGroup, total);
            }

            chart1.Series.Add(series);
        }
        public void LoadCityChart()
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
        SELECT 
            city,
            COUNT(*) AS total
        FROM Patient
        GROUP BY city
        ORDER BY total DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            chart2.Series.Clear();
            chart2.Titles.Clear();

            chart2.Titles.Add("Patients by City");

            Series series = new Series("Cities");
            series.ChartType = SeriesChartType.Column; // or Pie for dashboard

            foreach (DataRow row in dt.Rows)
            {
                string city = row["city"].ToString();
                int total = Convert.ToInt32(row["total"]);

                series.Points.AddXY(city, total);
            }

            chart2.Series.Add(series);
        }

        private void panelCard1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadAnalist()
        {
            string connectionString = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Patient";

                SqlCommand cmd = new SqlCommand(query, con);

                int count = (int)cmd.ExecuteScalar();
                string query2 = "SELECT SUM(total_amount) FROM Order_Table ";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                decimal sum = Convert.ToDecimal(cmd2.ExecuteScalar());

                string query3 = "SELECT TOP 1 country from Patient";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                string totalcountry = cmd3.ExecuteScalar().ToString();

                string query4 = "SELECT MAX(total_amount) FROM Order_Table";
                SqlCommand cmd4 = new SqlCommand(query4, con);
                decimal TopAmount = Convert.ToDecimal(cmd4.ExecuteScalar());

                label3.Text = count.ToString();
                label4.Text = sum.ToString();
                label5.Text = totalcountry.ToString();
                label6.Text = TopAmount.ToString();
            }

        }
    }
}

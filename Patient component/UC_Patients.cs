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

namespace e_commerce_NYC
{
    public partial class UC_Patients : UserControl
    {
        private DataTable fullData;
        private int pageSize = 10; // câte rânduri pe pagină
        private int currentPage = 0;
        public UC_Patients()
        {
            InitializeComponent();
            LoadPatients();
        }
        private void LoadPatients()
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
            switch(guna2ComboBox1.SelectedItem.ToString())
            {
                case "Date":
                    {
                        string connSt=@"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
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

        private void add_Click(object sender, EventArgs e)
        {

        }

        private void edit_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}

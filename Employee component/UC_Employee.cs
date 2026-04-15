using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using System.IO;

namespace e_commerce_NYC
{
    public partial class UC_Employee : UserControl
    {
        private DataTable fullData;
        private int pageSize = 10; // câte rânduri pe pagină
        private int currentPage = 0;
        public UC_Employee()

        {

            InitializeComponent();
            LoadEmployees();
            //LoadRoles();
            LoadChartFromDB();
            Loadtop();
            LoadAnalist();
            LoadSalaryDistributionChart();
           

        }
        private void LoadEmployees()
        {
            EUROPTICADataSet dataset = new EUROPTICADataSet();
            EUROPTICADataSetTableAdapters.EmployeeTableAdapter adapter =
                new EUROPTICADataSetTableAdapters.EmployeeTableAdapter();
            adapter.Fill(dataset.Employee);
            fullData = dataset.Employee;
            ShowPage(0);
            if (!dataGridView1.Columns.Contains("btnDelete"))
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

                dataGridView1.Columns.Add(deleteBtn);
                dataGridView1.Columns.Add(editBtn);
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnDelete"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_employee"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Ești sigur că vrei să ștergi?",
                    "Confirmare",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    DeleteEmployee(id);
                    LoadEmployees(); // reîncarcă datele
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["btnEdit"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_employee"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Ești sigur că vrei să editezi?",
                    "Confirmare",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    uc_employee_edit form = new uc_employee_edit(id);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadEmployees();
                    } // refresh datele
                }
            }
        }

        private void DeleteEmployee(int id)
        {
            string connStr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Employee WHERE id_employee = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea angajatului: " + ex.Message);
            }
        }
        
        private void EditEmployee(int id)
        {
            string connStr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Employee SET ... WHERE id_employee = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea angajatului: " + ex.Message);
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

            dataGridView1.DataSource = pageData;
            dataGridView1.ScrollBars = ScrollBars.None;

            // Ajustează înălțimea automată
            int height = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                height += row.Height;
            dataGridView1.Height = height;

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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.employeeTableAdapter.FillBy(this.eUROPTICADataSet.Employee);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            uc_employee_add form = new uc_employee_add();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
                MessageBox.Show("Employee added successfully!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            uc_employee_delete form = new uc_employee_delete();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
                MessageBox.Show("Employee deleted successfully!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
                return;
            uc_employee_edit_id form = new uc_employee_edit_id();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
                MessageBox.Show("Employee updated successfully!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                LoadEmployees();
                return;
            }

            SearchEmployees(textBox1.Text);
        
          }
        private void SearchEmployees(string text)
        {
            text = text.Replace("'", "''");

            DataView dv = fullData.DefaultView;

            dv.RowFilter =
                $"first_name LIKE '%{text}%' OR last_name LIKE '%{text}%'";

            dataGridView1.DataSource = dv;
            if(dv==null)
                MessageBox.Show("No results found!");
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btnDelete"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_employee"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Ești sigur că vrei să ștergi?",
                    "Confirmare",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    DeleteEmployee(id);
                    LoadEmployees(); // reîncarcă datele
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["btnEdit"].Index && e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_employee"].Value);

                DialogResult confirm = MessageBox.Show(
                    "Ești sigur că vrei să editezi?",
                    "Confirmare",
                    MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    uc_employee_edit form = new uc_employee_edit(id);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadEmployees();
                    } // refresh datele
                }
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                LoadEmployees();
                return;
            }

            SearchEmployees(textBox1.Text);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        //public void LoadRoles()
        //{
        //    try
        //    {
        //        string connStr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
        //        using (SqlConnection conn = new SqlConnection(connStr))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand(
        //                "SELECT role_name FROM Employee_Role", conn);
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
        //            dataGridView2.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error : " + ex.Message);
        //    }
        //}

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        //chard from my database it is about roles-number of people 
        private void LoadChartFromDB()
        {
            chart1.Series.Clear();

            Series series = new Series("EmployeesByRole");
            series.ChartType = SeriesChartType.Column;

            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            SELECT role_name, COUNT(E.id_role) AS TotalEmployees 
            FROM Employee_Role R
            JOIN Employee E ON R.id_role=E.id_role
            GROUP BY role_name", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string role = reader[0].ToString();
                    int count = Convert.ToInt32(reader[1]);

                    series.Points.AddXY(role, count);
                }
            }

            chart1.Series.Add(series);
        }
        public void Loadtop()
        {
            try
            {
                string connStr = @"Data Source = Adina\SQLEXPRESS; Initial Catalog = EUROPTICA; Integrated Security=True; TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT TOP 5 first_name,last_name,salary FROM Employee ORDER BY salary DESC;", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView3.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadAnalist()
        {
            string connectionString = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Employee";

                SqlCommand cmd = new SqlCommand(query, con);

                int count = (int)cmd.ExecuteScalar();
                string query2 = "SELECT AVG(salary) FROM Employee";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                decimal average = Convert.ToDecimal(cmd2.ExecuteScalar());

                string query3 = "SELECT SUM(salary) FROM Employee";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                decimal totalSalary = Convert.ToDecimal(cmd3.ExecuteScalar());

                string query4 = "SELECT TOP 1 salary FROM Employee ORDER BY salary DESC";
                SqlCommand cmd4 = new SqlCommand(query4, con);
                decimal TopS = Convert.ToDecimal(cmd4.ExecuteScalar());

                label3.Text = count.ToString();
                label4.Text = average.ToString();
                label6.Text = totalSalary.ToString();
                label8.Text = TopS.ToString();
            }
            
        }

        private void pdf_Click(object sender, EventArgs e)
        {
            string url = "http://localhost/ReportServer?/YourFolder/YourReportName&rs:Command=Render&rs:Format=PDF";

            WebClient client = new WebClient();

            byte[] data = client.DownloadData(url);

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF file (*.pdf)|*.pdf";
            save.FileName = "Report.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(save.FileName, data);
                MessageBox.Show("Report downloaded successfully!");
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
        private void LoadSalaryDistributionChart()
        {
            chart2.Series.Clear();

            Series series = new Series("Salary Distribution");
            series.ChartType = SeriesChartType.Column;

            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                CASE 
                    WHEN salary < 5000 THEN '0-5000'
                    WHEN salary BETWEEN 5000 AND 10000 THEN '5000-10000'
                    ELSE '10000+'
                END AS SalaryRange,
                COUNT(*) AS TotalPeople
            FROM Employee
            GROUP BY 
                CASE 
                    WHEN salary < 5000 THEN '0-5000'
                    WHEN salary BETWEEN 5000 AND 10000 THEN '5000-10000'
                    ELSE '10000+'
                END", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string range = reader[0].ToString();
                    int count = Convert.ToInt32(reader[1]);

                    series.Points.AddXY(range, count);
                }
            }

            chart2.Series.Add(series);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(comboBox1.SelectedItem.ToString())
            {
                case "Role":
                    LoadDateByRole();
                    break;
                case "Date":
                    LoadDateByDate();
                    break;
                case "Status":
                    LoadDateByStatus();
                    break;
                default:
                    LoadEmployees();
                    break;
            }
        }
        private void LoadDateByRole()
        {
            string connSt=@" Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
            using (SqlConnection conn=new SqlConnection(connSt))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"Select *From Employee
E                   Order by id_role", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void LoadDateByDate()
        {
            string connSt = @" Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"Select *From Employee
E                   Order by hire_date", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void LoadDateByStatus()
        {
            string connSt = @" Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connSt))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"Select *From Employee
E                   Order by is_active", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void panelPagination_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

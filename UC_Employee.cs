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
    public partial class UC_Employee : UserControl
    {
        private DataTable fullData;
        private int pageSize = 10; // câte rânduri pe pagină
        private int currentPage = 0;
        public UC_Employee()

        {

            InitializeComponent();
            LoadEmployees();
           

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
    }
}

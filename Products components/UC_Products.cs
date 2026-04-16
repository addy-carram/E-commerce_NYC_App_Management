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

            SearchPatients(guna2TextBox1.Text);
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

        private void SearchPatients(string text)
        {
            text = text.Replace("'", "''");

            DataView dv = fullData.DefaultView;

            dv.RowFilter =
                $"model_name LIKE '%{text}%' OR producer LIKE '%{text}%' OR type_name LIKE '%{text}%'";

            guna2DataGridView1.DataSource = dv;
            if (dv == null)
                MessageBox.Show("No results found!");
        }
    }
}

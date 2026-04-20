using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace e_commerce_NYC
{
    public partial class UC_Reports : UserControl
    {
        public UC_Reports()
        {
            InitializeComponent();
        }
        private DataTable GetData(string query)
        {
            string connStr = @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dt);
            }

            return dt;
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                
                switch (guna2ComboBox1.SelectedItem.ToString()) {

                    case "Products":
                        {
                            reportViewer1.LocalReport.ReportEmbeddedResource = "e_commerce_NYC.Reports.Report_Products.rdlc";
                            DataTable dt = GetData("SELECT * FROM Product");
                            reportViewer1.LocalReport.DataSources.Add(
                                new ReportDataSource("DataSet1", dt));
                            break;
                        }
                    case "Orders":
                        {
                            reportViewer1.LocalReport.ReportEmbeddedResource = "e_commerce_NYC.Reports.Report_Orders.rdlc";
                            DataTable dt = GetData("SELECT \r\n    PT.type_name AS Categorie,\r\n    COUNT(DISTINCT O.id_order) AS Nr_Comenzi,\r\n    COUNT(DISTINCT O.id_patient) AS Nr_Clienti_Unici,\r\n    SUM(O.total_amount) AS Vanzari_Totale,\r\n    AVG(O.total_amount) AS Valoare_Medie_Comanda,\r\n    MIN(O.total_amount) AS Comanda_Minima,\r\n    MAX(O.total_amount) AS Comanda_Maxima\r\nFROM Order_Table O\r\nJOIN Product P ON O.id_product = P.id_product\r\nJOIN Product_Type PT ON P.id_product_type = PT.id_product_type\r\nWHERE O.id_order_status IN (3, 4) -- ready/completed\r\nGROUP BY PT.type_name;");
                            reportViewer1.LocalReport.DataSources.Add(
                                new ReportDataSource("DataSet1", dt));
                            break;
                        }
                    case "Employee":
                        {
                            reportViewer1.LocalReport.ReportEmbeddedResource = "e_commerce_NYC.Reports.Report_Employee.rdlc";
                            DataTable dt = GetData("SELECT * FROM Employee e JOIN Employee_Role er On e.id_role=er.id_role");
                            reportViewer1.LocalReport.DataSources.Add(
                                new ReportDataSource("DataSet1", dt));
                            break;
                        }
                    case "Patient":
                        {
                            reportViewer1.LocalReport.ReportEmbeddedResource = "e_commerce_NYC.Reports.Report_Patients.rdlc";
                            DataTable dt = GetData("SELECT o.id_order,p.first_name+' '+p.last_name as full_name, orr.status_name,o.total_amount FROM Order_Table o Join Order_Status orr ON orr.id_order_status=o.id_order_status " +
                                "JOIN Patient p ON o.id_patient=p.id_patient");

                            reportViewer1.LocalReport.DataSources.Add(
                                new ReportDataSource("DataSet3", dt));
                            break;
                        }



                }
                reportViewer1.RefreshReport();


            }
            catch (Exception ex) {
                MessageBox.Show("error reports" + ex.Message);
                    }            
        }
    }
}

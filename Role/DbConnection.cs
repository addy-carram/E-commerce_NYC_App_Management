
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace e_commerce_NYC
{
    public class DbConnection
    {
        public static string connStr =
            @"Data Source=Adina\SQLEXPRESS;Initial Catalog=EUROPTICA;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_NYC.Products_components
{
    public class Product
    {
        public int id { get; set; }
        public string producer { get; set; }
        public string model_name { get; set; }
        public int id_type { get; set; }
        public string type_material { get; set; }
        public decimal total_price { get; set; }
        public int stock_quantity { get; set; }
        public bool is_active { get; set; }
    }
}

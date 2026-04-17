using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_NYC
{
    public class Order
    {
        public int id_order { get; set; }
        public int id_patient { get; set; }
        public int id_product { get; set; }
        public int id_employee { get; set; }
        public DateTime order_date { get; set; }
        public int id_order_status { get; set; }
        public decimal total_amount { get; set; }
        public int id_payment_status { get; set; }
        public int id_payment_method { get; set; }
        public string notes { get; set; }
    }
}

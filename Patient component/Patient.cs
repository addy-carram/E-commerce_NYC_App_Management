using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_NYC
{
    public class Patient
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_birth { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string idnp { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public bool is_active { get; set; }
    }
}

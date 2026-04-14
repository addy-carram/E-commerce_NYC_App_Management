using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace e_commerce_NYC
{
    internal class Employee
    {
      public int id {get;set;}
        public string first_name {get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string d_birth {get;set;}
        public decimal salary { get; set; }
       public bool is_active { get; set; }
        public string role { get; set; }



    }
}

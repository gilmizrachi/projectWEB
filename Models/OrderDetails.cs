using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class OrderDetails
    {
        public int order_id { get; set; }
        public string item_name { get; set; }
        public int item_quantity { get; set; }
        public int item_price { get; set; }
        public string item_category { get; set; }
    }
}

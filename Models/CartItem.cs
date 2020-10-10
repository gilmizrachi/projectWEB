using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    [NotMapped]
    public class CartItem
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ProductTotal {
            get
            {
                return Price * Quantity;
            }
        }
    }
}

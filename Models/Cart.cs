using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    [NotMapped]
    public class Cart
    {
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    [NotMapped]
    public class MyDetails
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public string category { get; set; }
    }
}

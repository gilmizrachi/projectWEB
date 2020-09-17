using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public Item item { get; set; }

        public string CustomerName { get; set; }

        public string Comment { get; set; }
        public DateTime PublishTime { get; set; }
    }
}

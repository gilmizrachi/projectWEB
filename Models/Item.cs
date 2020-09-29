using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Data
{
    public class Item
    {
        public int id { get; set; }

        public string ItemName { get; set; }
        public int price { get; set; }

        public string ItemDevision { get; set; }
        public byte[] ItemImage { get; set; }

        public string Description { get; set; }

        public int amount { get; set; }
        public virtual ICollection<Reviews> Comments { get; set; }
    }
}

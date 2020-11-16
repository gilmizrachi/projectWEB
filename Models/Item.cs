using projectWEB.Models;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace projectWEB.Data
{
    public class Item
    {
        public int id { get; set; }
        [Display(Name = "Product name")]

        public string ItemName { get; set; }
        public int price { get; set; }
        
        public string ItemDevision { get; set; }
        public byte[] ItemImage { get; set; }

        public string Description { get; set; }

        public int amount { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<Reviews> Comments { get; set; }
    }

}

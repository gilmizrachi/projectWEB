using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace projectWEB.Models
{
    [NotMapped]
    public class CategoryWithChildren
    {
        public Category Category { get; set; }
        public ICollection<Category> Children { get; set; }
    }
}

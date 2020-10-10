using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    [NotMapped]
    public class ProductRating
    {
        public int TotalRatings { get; set; }
        public int AverageRating { get; set; }
    }
}

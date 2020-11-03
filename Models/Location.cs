using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Display(Name ="Store Name")]
        public string LocationName { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}

using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Location : BaseEntity
    {
        [Required(ErrorMessage = "Please enter city name")]
        [Display(Name = "City Name")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter city latitude")]
        public double Lat { get; set; }

        [Required(ErrorMessage = "Please enter city longitude ")]
        public double Lng { get; set; }
      
    }
}

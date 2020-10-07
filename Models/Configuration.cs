using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Configuration
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public int ConfigurationType { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
    public enum ConfigurationTypes
    {
        Site = 1,
        Sliders = 2,
        Promotions = 3,
        SocialLinks = 4,
        Other = 5
    }
}

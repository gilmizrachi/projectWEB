using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace projectWEB.Models
{
    public class RegisteredUsers : IdentityUser
    {
        [Required(ErrorMessage = "Full name required")]
        [MinLengthAttribute(3)]
        [MaxLength(20)]
        public string FullName { get; set; }

        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }

        public DateTime? RegisteredOn { get; set; }

    }
}

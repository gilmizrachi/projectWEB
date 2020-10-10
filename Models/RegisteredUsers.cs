using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;

namespace projectWEB.Models
{
    public class RegisteredUsers : IdentityUser
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
        public DateTime? RegisteredOn { get; set; }

    }
}

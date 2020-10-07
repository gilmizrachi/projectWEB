using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class RegisteredUsers : IdentityUser
    {
        
        [Required(ErrorMessage = "This field is required")]
        [MinLengthAttribute(3)]
        [MaxLength(15)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [MinLengthAttribute(3)]
        [MaxLength(20)]
        public string FullName { get; set; }

        
        [MinLengthAttribute(8)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [MinLengthAttribute(8)]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public MemberType MemberType { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<RegisteredUsers> manager)
        //{
        //    var authenticationType = CookieAuthenticationDefaults.;
        //    var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);

        //    userIdentity.AddClaim(new Claim("Email", Email));
        //    userIdentity.AddClaim(new Claim("Picture", this.Picture != null ? this.Picture.URL : string.Empty));
        //    return userIdentity;
        //}
    }
    public enum MemberType
    {
        BasicUser,
        SalesPerson,
        Supervisor,
        Admin
    }
}

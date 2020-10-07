using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace projectWEB.App_Start
{
    public class CustomClaimsPrincipalFactory :
   UserClaimsPrincipalFactory<RegisteredUsers,
   IdentityRole>
    {
        public CustomClaimsPrincipalFactory(
           UserManager<RegisteredUsers> userManager,
           RoleManager<IdentityRole> roleManager,
           IOptions<IdentityOptions> optionsAccessor) :
              base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(RegisteredUsers user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Email", user.Email.ToString()));
            //identity.AddClaim(new Claim("Picture", user.Picture != null ? user.Picture.URL : string.Empty));
            return identity;
        }
    }
}

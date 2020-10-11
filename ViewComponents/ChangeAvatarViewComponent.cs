using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Controllers;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class ChangeAvatarViewComponent : ViewComponent
    {
        private readonly UserManager<RegisteredUsers> _usermanager;
        public ChangeAvatarViewComponent(UserManager<RegisteredUsers> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userId = _usermanager.GetUserId(UserClaimsPrincipal);

            ProfileDetailsViewModel model = new ProfileDetailsViewModel
            {
                User = await _usermanager.FindByIdAsync(userId)
            };

            return View("_ChangeAvatar", model);

        }
    }
}

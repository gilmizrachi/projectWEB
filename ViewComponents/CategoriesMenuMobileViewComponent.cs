using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Controllers;
using projectWEB.Data;
using projectWEB.Helpers;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class CategoriesMenuMobileViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CategoriesMenuViewModel model = new CategoriesMenuViewModel();

            var categories = CategoriesService.Instance.GetCategories();

            if (categories != null && categories.Count > 0)
            {
                //remove uncategorized category from categories list.
                categories = categories.Where(x => x.ID != 1).ToList();

                model.CategoryWithChildrens = CategoryHelpers.MakeCategoriesHierarchy(categories);
            }
            return View("_CategoriesMenuMobile", model);
        }
    }
}

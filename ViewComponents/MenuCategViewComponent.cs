using Microsoft.AspNetCore.Mvc;
using projectWEB.Helpers;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class MenuCategViewComponent : ViewComponent
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

            return View("_CategoriesMenu", model);
        }
    }
}

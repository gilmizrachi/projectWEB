using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Controllers;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class FeaturedCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int recordSize = 8)
        {
            var categories = CategoriesService.Instance.GetFeaturedCategories(recordSize: recordSize);

            return View("_FeaturedCategoriesHomeSection", categories);
        }
    }
}

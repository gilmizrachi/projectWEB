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
    public class ProductsByFeaturedCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int recordSize = 5)
        {
            ProductsByFeaturedCategoriesViewModel model = new ProductsByFeaturedCategoriesViewModel
            {
                Categories = CategoriesService.Instance.GetFeaturedCategories(recordSize: recordSize)
            };

            return View("_ProductsByFeaturedCategories", model);
        }
    }
}

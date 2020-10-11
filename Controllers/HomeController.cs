using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectWEB.Helpers;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using projectWEB.ViewModels;

namespace projectWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //if (Request.Path.ToString().Equals("/"))
            //{
            //    return Redirect(Url.Home());
            //}

            return View(new PageViewModel());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string category, string q, decimal? from, decimal? to, string sortby, int? pageNo, int? recordSize)
        {
            recordSize = recordSize ?? (int)RecordSizeEnums.Size20;

            ProductsViewModel model = new ProductsViewModel
            {
                Categories = CategoriesService.Instance.GetCategories()
            };

            if (!string.IsNullOrEmpty(category))
            {
                var selectedCategory = CategoriesService.Instance.GetCategoryByName(category);

                if (selectedCategory == null) return NotFound();
                else
                {
                    model.CategoryID = selectedCategory.ID;
                    model.CategoryName = selectedCategory.SanitizedName;
                    model.SelectedCategory = selectedCategory;

                    model.SearchedCategories = CategoryHelpers.GetAllCategoryChildrens(selectedCategory, model.Categories);
                }
            }

            model.SearchTerm = q;
            model.PriceFrom = from;
            model.PriceTo = to;
            model.SortBy = sortby;
            model.PageSize = recordSize;

            var selectedCategoryIDs = model.SearchedCategories != null ? model.SearchedCategories.Select(x => x.ID).ToList() : null;

            model.Products = ProductsService.Instance.SearchProducts(selectedCategoryIDs, model.SearchTerm, model.PriceFrom, model.PriceTo, model.SortBy, pageNo, recordSize.Value, out int count);

            model.Pager = new Pager(count, pageNo, recordSize.Value);

            return View(model);
        }
        
    }
}

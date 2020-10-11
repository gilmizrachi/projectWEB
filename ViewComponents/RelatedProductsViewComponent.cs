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
    public class RelatedProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int categoryID, int recordSize = (int)RecordSizeEnums.Size6)
        {
            RelatedProductsViewModel model = new RelatedProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(new List<int>() { categoryID }, null, null, null, null, 1, recordSize, out int count)
            };

            if (model.Products == null || model.Products.Count < (int)RecordSizeEnums.Size6)
            {
                //the realted products are less than the specfified RelatedProductsRecordsSize, so instead show featured products
                model.Products = ProductsService.Instance.SearchFeaturedProducts(recordSize);
                model.IsFeaturedProductsOnly = true;
            }

            return View("_RelatedProducts", model);
        }
    }
}

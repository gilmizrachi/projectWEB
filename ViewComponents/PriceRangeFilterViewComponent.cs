using Microsoft.AspNetCore.Mvc;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class PriceRangeFilterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(decimal? priceFrom, decimal? priceTo)
        {
            var model = new PriceRangeFilterViewModel
            {
                PriceFrom = priceFrom,
                PriceTo = priceTo,

                MaxPrice = ProductsService.Instance.GetMaxProductPrice()
            };

            return View("_PriceRangeFilter", model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using projectWEB.Models;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class HomeSlidersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            HomeSlidersViewModel model = new HomeSlidersViewModel
            {
                SlidersConfigurations = ConfigurationsService.Instance.GetConfigurationsByType((int)ConfigurationTypes.Sliders)
            };

            return View("_HomeSliders", model);
        }
    }
}

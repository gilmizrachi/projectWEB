using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index1()
        {
            ViewBag.test2 =  HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            ViewBag.test = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;

            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }
        public IActionResult OhhNo(int id)
        {   ViewBag.ErrorId = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return RedirectToAction("ohhNo");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

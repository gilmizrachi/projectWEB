using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
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
        [HttpGet]
        public IActionResult test()
        {
            return View();
        }

       /* [HttpPost]
        public async Task<IActionResult> test(IFormFile file)
        {
            return View(file);
        }
         private string ProcessImage(string croppedImage)

        {

            string filePath = String.Empty;

            try
            {

                string base64 = croppedImage;

                byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

                filePath = "/Images/Photo/Emp-" + Guid.NewGuid() + ".jpg";

                using (FileStream stream = new FileStream(Server.MapPath(filePath), FileMode.Create))

                {

                    stream.Write(bytes, 0, bytes.Length);

                    stream.Flush();

                }

            }

            catch (Exception ex)

            {

                string st = ex.Message;

            }

            return filePath;

        }*/
    }
}

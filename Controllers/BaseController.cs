using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectWEB.Data;

namespace projectWEB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly projectWEBContext _context;
        public BaseController(projectWEBContext context)
        {
            _context = context;
        }
        //public void setCategoriesMenu()
        //{
        //    var modelCategory = _context.Categories.ToList();
        //    ViewBag.modelCategory = modelCategory;
        //    ViewBag.cart = HttpContext.Session.GetString("cart");
        //    ViewBag.size = HttpContext.Session.GetInt32("size");
        //    ViewBag.total = HttpContext.Session.GetString("total");
        //}
    }
}
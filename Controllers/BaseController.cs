using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        public void setCategoriesMenu()
        {
            var modelCategory = _context.Category.ToList();
            ViewBag.modelCategory = modelCategory;
            ViewBag.cart = HttpContext.Session.GetString("cart");
            ViewBag.size = HttpContext.Session.GetInt32("size");
            ViewBag.total = HttpContext.Session.GetString("total");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            setCategoriesMenu();
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
        }
    }
}
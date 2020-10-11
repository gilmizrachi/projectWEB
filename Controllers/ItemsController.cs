using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;
using Microsoft.AspNetCore.Http;
using projectWEB.Migrations;

namespace projectWEB.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly projectWEBContext _context;

        public ItemsController(projectWEBContext context)
        {
            _context = context;
        }

        // GET: Items

        /*
        public  IActionResult Index()
        {
            return View();
        } */
        [AllowAnonymous]
        public IActionResult Info()
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
           /*  var locations =[ ['<div class="infobox"><h3 class="title"><a href="#">Here we are</a></h3><span>Rishon Le Zion / Street</span><span> +972 3 444 55 66</span></div>',
            31.970394,
            34.771959,
            1]]; 
            ViewBag.location1.title= "info";
            ViewBag.location1.latt = 31.970394;
            ViewBag.location1.longt = 34.771959;*/

            return View();
        }


       /* public void UpdateCookie(IEnumerable<Item> data)
        {
            var analytics = from d in data
                            select new { id = d.id, category = d.ItemDevision };
            var keepme = 
            if (Request.Cookies["collector"] != null)
            {
                // Response.Cookies.Append(analytics.ToList())
                CookieOptions cookieop = new CookieOptions();
                cookieop.Expires = DateTime.Now.AddMinutes(3);

                Response.Cookies.Append("collector",,)
            }
        }
       */
        public async Task<IActionResult> Item_Details(int id)
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.id = HttpContext.Session.GetString("userId");
            return View( _context.Item.Where(u=>u.id==id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string? Phrase,string? byname,string? category,int byprice) //change the int in case of truble int to 
        {
            var usr= HttpContext.User.FindFirst(x => x.Type == ClaimTypes.SerialNumber)?.Value;
            var Profile = _context.AlsoTry.Where(u => u.registeredUsers.id.ToString() == usr && u.IsActive).First();
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            if (Phrase != null) {
            var result = _context.Item.Where(str=>str.ItemName.Contains(Phrase)||str.ItemDevision.Contains(Phrase) || str.Description.Contains(Phrase));
                Profile.AddSearchedPhrase(Phrase);
                _context.Update(Profile);
                await _context.SaveChangesAsync();
                return View("Mainshop", await result.ToListAsync());
            }
            else if(byname!=null||category!=null||byprice>0)
            {
                var localresult = _context.Item.Where(str => str.amount > 0);//ItemName.Contains(byname) || str.ItemDevision.Contains(category) || str.price.CompareTo(byprice)<0);
                List<Item> result = new List<Item>();
                result.AddRange(localresult.Distinct());
                if (byprice > 0){
                    Profile.AddSearchByPrice(byprice);
                    //result.AddRange(localresult.Where(p => p.price < byprice));
                    result.RemoveAll(p => p.price > byprice);
                }
                if (category != null){
                    Profile.AddSearchedPhrase(category);
                    //result.AddRange(localresult.Where(p => p.ItemDevision.Contains(category)));
                    var comp = result.Where(p => p.ItemDevision.Contains(category));
                    result.Clear();
                    result.AddRange(comp);
                }
                if (byname != null) { 
                    Profile.AddSearchedPhrase(byname);
                    //result.AddRange(localresult.Where(p => p.ItemName.Contains(byname)));
                    var comp = result.Where(p => p.ItemName.Contains(byname));
                    result.Clear();
                    result.AddRange(comp);
                }
                _context.Update(Profile);
                await _context.SaveChangesAsync();
                return View("Mainshop",  result.Distinct());
            }
            return View("Mainshop");


        }
       
        [HttpPost]
        public async Task<IActionResult> Sort(string Sortby)
        {

            if (Sortby == "1") {return View( await _context.Item.OrderByDescending(p => p.price).ToListAsync()); }
            else if (Sortby == "2") { return View( await _context.Item.OrderBy(p => p.price).ToListAsync()); }
            else if (Sortby == "3") { return View( await _context.Item.OrderByDescending(p => p.id).ToListAsync()); }
            else if (Sortby == "4") { return View( await _context.Item.OrderBy(p => p.id).ToListAsync()); }
            else { return View("Mainshop"); }

        }
        public IActionResult Index2()
        {
            return View();
        }
       // [Authorize]
        public async Task<IActionResult> Mainshop()
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            // HttpContext.Session.SetString()
            return View(await _context.Item.ToListAsync());
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="SalesPerson,Supervisor")]
        public async Task<IActionResult> Create([Bind("id,ItemName,price,ItemDevision,Description,amount")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        [Authorize(Roles ="SalesPerson,Supervisor,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        /*
                // GET: Items/Details/5
                public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var item = await _context.Item
                        .FirstOrDefaultAsync(m => m.id == id);
                    if (item == null)
                    {
                        return NotFound();
                    }

                    return View(item);
                }

                // GET: Items/Create
                public IActionResult Create()
                {
                    return View();
                }

                // POST: Items/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("id,ItemName,price,ItemDevision,Description,amount")] Item item)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(item);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(item);
                }

                // GET: Items/Edit/5
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var item = await _context.Item.FindAsync(id);
                    if (item == null)
                    {
                        return NotFound();
                    }
                    return View(item);
                }

                // POST: Items/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("id,ItemName,price,ItemDevision,Description,amount")] Item item)
                {
                    if (id != item.id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(item);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ItemExists(item.id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    return View(item);
                }

                // GET: Items/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var item = await _context.Item
                        .FirstOrDefaultAsync(m => m.id == id);
                    if (item == null)
                    {
                        return NotFound();
                    }

                    return View(item);
                }

                // POST: Items/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var item = await _context.Item.FindAsync(id);
                    _context.Item.Remove(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool ItemExists(int id)
                {
                    return _context.Item.Any(e => e.id == id);
                }
        */
    }
}

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
using Microsoft.AspNetCore.Hosting;
using projectWEB.Migrations;
using Newtonsoft.Json;
using System.IO;


namespace projectWEB.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly projectWEBContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        public ItemsController(projectWEBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }


        [AllowAnonymous]
        public IActionResult Info()
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;


            return View();
        }
/*
        private async void ValidateProfile(RegisteredUsers registeredUsers)
        {
            if (_context.AlsoTry.Where(u => u.registeredUsers == registeredUsers && u.IsActive == true).Count() > 0)
                return;
            var profile = new AlsoTry() { registeredUsers = registeredUsers };
            profile.Transaction = _context.Transaction.Where(u => u.Customer.id == registeredUsers.id && u.Status == 0).First();

            var c = new AlsoTriesController(_context);
            c.NewProfile(profile);

            _context.SaveChanges();

        }
             public void UpdateCookie(IEnumerable<Item> data)
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
            var it = _context.Item.Where(u => u.id == id);
            var usr = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.SerialNumber)?.Value;
            if (!_context.Transaction.Any(i => i.Customer.id.ToString() == usr&&i.Status==0)) {
             Transaction transaction = new Transaction() { CustomerId = Int32.Parse(HttpContext.Session.GetString("userId")), SumPrice = 0 };
            }
            if (_context.AlsoTry.Where(u => u.registeredUsers.id.ToString() == usr && u.IsActive).Any())
            {
                var Profile = _context.AlsoTry.Where(u => u.registeredUsers.id.ToString() == usr && u.IsActive).First();
                Profile.V_Items.Add(it.First());
                Profile.V_ItemNo += 1;
                _context.Update(Profile);
                await _context.SaveChangesAsync();
            }
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.id = HttpContext.Session.GetString("userId");
            
            return View(it);
        }
        [Authorize(Roles = "SalesPerson,Supervisor,Admin")]
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

        public void setCategoriesMenu()
        {
            var modelCategory = _context.Category.ToList();
            ViewBag.modelCategory = modelCategory;
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

        [AllowAnonymous]
        public async Task<IActionResult> Manage()
        {
            var bullshit = from a in _context.Transaction.Include(p => p.Cart)
                           where a.Status != 0
                           select new { sale_time = a.TranscationDate.ToShortDateString(), value = a.SumPrice };//value = a.Cart.Count() };
            var da = bullshit.ToList();
            var T_statistics = new int[] { _context.Transaction.Where(a => a.Status != Status.Pending).Count(), _context.Transaction.Where(i => i.Status == Status.Pending).Count()};
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            ViewBag.DataPoints = JsonConvert.SerializeObject(da);
            ViewBag.statis = JsonConvert.SerializeObject(T_statistics);
            ViewBag.usersList = await _context.RegisteredUsers.OrderByDescending(a => a.id).ToListAsync();
            ViewBag.Waiting = await _context.Transaction.Where(a => a.Status == Status.Approved).ToListAsync();
            return View();
            
        }
        // [Authorize]
        public async Task<IActionResult> Mainshop()
        {
            var usr = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.SerialNumber)?.Value;

            if (!_context.Transaction.Any(i => i.Customer.id.ToString() == usr && i.Status == 0))
            {
               // Transaction transaction = new Transaction() { CustomerId = Int32.Parse(HttpContext.Session.GetString("userId")), SumPrice = 0 };
                Transaction transaction = new Transaction() { CustomerId = Int32.Parse(HttpContext.User.FindFirst(x => x.Type == ClaimTypes.SerialNumber).Value), SumPrice = 0 };
                _context.Add(transaction);
                await _context.SaveChangesAsync();
            }
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            ViewBag.ItemVal = JsonConvert.SerializeObject(await _context.Item.ToListAsync());
            // HttpContext.Session.SetString()
            setCategoriesMenu();
            return View(await _context.Item.ToListAsync());
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="SalesPerson,Supervisor,Admin")]
        public async Task<IActionResult> Create([Bind("id,ItemName,price,ItemDevision,Description,amount")] Item item, List<IFormFile> FormFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                if (FormFile != null)
                {
                    for (int i = 0; i < FormFile.Count() && i < 4; i++)
                    {
                        var filePath = hostingEnvironment.WebRootPath + "/upload/items/" + item.id + "-" + i + ".jpg";
                        await FormFile[i].CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }
                }

                FacebookApi fc = new FacebookApi();
                fc.PublishMessage(item.ItemName + " is the newest product on our shopping site.\n"
                     + "Description: " + item.Description + ".\n"
                      + "Only: " + item.amount + "units remained! hurry up!\n"
                       + "Price: " + item.price + "$");

                return RedirectToAction(nameof(Manage));
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
            itemview harta = new itemview(item);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ItemName,price,ItemDevision,Description,amount,FormFile")] Item item,IFormFile FormFile0, IFormFile FormFile1, IFormFile FormFile2, IFormFile FormFile3)//, string FormFile)  [Bind("id,ItemName,price,ItemDevision,Description,amount,FormFile")] 
        {

           
                 if (FormFile0 != null)
               {
                var filePath = hostingEnvironment.WebRootPath + "/upload/items/" + item.id + "-0.jpg";
                await FormFile0.CopyToAsync(new FileStream(filePath, FileMode.Create));
               }
                if (FormFile1 != null)
                {
                    var filePath = hostingEnvironment.WebRootPath + "/upload/items/" + item.id + "-1.jpg";
                    await FormFile1.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
                if (FormFile2 != null)
                {
                    var filePath = hostingEnvironment.WebRootPath + "/upload/items/" + item.id + "-2.jpg";
                    await FormFile0.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
                if (FormFile3 != null)
                {
                    var filePath = hostingEnvironment.WebRootPath + "/upload/items/" + item.id + "-3.jpg";
                    await FormFile3.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
            
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
                return RedirectToAction(nameof(Manage));
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
        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.id == id);
        }
    }
}

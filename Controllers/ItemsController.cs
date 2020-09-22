using System;
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


namespace projectWEB.Controllers
{
    public class ItemsController : BaseController
    {

        public ItemsController(projectWEBContext context) : base(context)
        {
        }

        // GET: Items
        [Authorize]
        public async Task<IActionResult> index2()
        {
            return View(await _context.Item.ToListAsync());
            //return View();
        }
        /*
        public  IActionResult Index()
        {
            return View();
        } */
        [Authorize]
        public async Task<IActionResult> Item_Details(int id)
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            return View( _context.Item.Where(u=>u.id==id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string Phrase)
        {
            var result = _context.Item.Where(str => str.ItemName.Contains(Phrase) || str.ItemDevision.Contains(Phrase) || str.Description.Contains(Phrase));
            return View("Mainshop", await result.ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> Sort(string Sortby)
        {

            if (Sortby == "1") { return View(await _context.Item.OrderByDescending(p => p.price).ToListAsync()); }
            else if (Sortby == "2") { return View(await _context.Item.OrderBy(p => p.price).ToListAsync()); }
            else if (Sortby == "3") { return View(await _context.Item.OrderByDescending(p => p.id).ToListAsync()); }
            else if (Sortby == "4") { return View(await _context.Item.OrderBy(p => p.id).ToListAsync()); }
            else { return View("Mainshop"); }

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Item.ToListAsync());

            //return View();
        }
        [Authorize]
        public async Task<IActionResult> Mainshop()
        {
            setCategoriesMenu();
            return View(await _context.Item.ToListAsync());
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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

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
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class ItemsController : BaseController
    {

        public ItemsController(projectWEBContext context) : base(context)
        {
        }

       
        [Authorize]
        public async Task<IActionResult> Item_Details(int id)
        {
            ViewBag.membertype = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
            return View( _context.Products.Where(u=>u.ID==id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search(string Phrase)
        {
            //var result = _context.Products.Where(str => str..Contains(Phrase) || str.ItemDevision.Contains(Phrase) || str.Description.Contains(Phrase));
            //return View("Mainshop", await result.ToListAsync());
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Sort(string Sortby)
        {

            //if (Sortby == "1") { return View(await _context.Products.OrderByDescending(p => p.price).ToListAsync()); }
            //else if (Sortby == "2") { return View(await _context.Products.OrderBy(p => p.price).ToListAsync()); }
            //else if (Sortby == "3") { return View(await _context.Products.OrderByDescending(p => p.ID).ToListAsync()); }
            //else if (Sortby == "4") { return View(await _context.Products.OrderBy(p => p.ID).ToListAsync()); }
            //else { return View("Mainshop"); }    
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());

            //return View();
        }
        [Authorize]
        public async Task<IActionResult> Mainshop()
        {
            //setCategoriesMenu();
            return View(await _context.Products.ToListAsync());
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("id,ItemName,price,ItemDevision,Description,amount")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class AlsoTriesController : Controller
    {
        private readonly projectWEBContext _context;

        public AlsoTriesController(projectWEBContext context)
        {
            _context = context;
        }
        
        public async void NewProfile(AlsoTry profile)
        {
            AlsoTry alsoTry = profile;
            _context.Add(alsoTry);
            return;

        }
        //Get: Recommendation
        public async Task<IActionResult> Recomended()
        {
            var rating = _context.Reviews.Where(i => i.Rate > 0).OrderByDescending(i => i.Rate).GroupBy(i=>i.Item);

                          
                          
            return View(await _context.AlsoTry.ToListAsync());
        }

        // GET: AlsoTries
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlsoTry.ToListAsync());
        }

        // GET: AlsoTries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alsoTry = await _context.AlsoTry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alsoTry == null)
            {
                return NotFound();
            }

            return View(alsoTry);
        }

        // GET: AlsoTries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlsoTries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,S_Phrase,V_ItemNo,PriceLimits,IsActive")] AlsoTry alsoTry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alsoTry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alsoTry);
        }

        // GET: AlsoTries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alsoTry = await _context.AlsoTry.FindAsync(id);
            if (alsoTry == null)
            {
                return NotFound();
            }
            return View(alsoTry);
        }

        // POST: AlsoTries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,S_Phrase,V_ItemNo,PriceLimits,IsActive")] AlsoTry alsoTry)
        {
            if (id != alsoTry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alsoTry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlsoTryExists(alsoTry.Id))
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
            return View(alsoTry);
        }

        // GET: AlsoTries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alsoTry = await _context.AlsoTry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alsoTry == null)
            {
                return NotFound();
            }

            return View(alsoTry);
        }

        // POST: AlsoTries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alsoTry = await _context.AlsoTry.FindAsync(id);
            _context.AlsoTry.Remove(alsoTry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlsoTryExists(int id)
        {
            return _context.AlsoTry.Any(e => e.Id == id);
        }
    }
}

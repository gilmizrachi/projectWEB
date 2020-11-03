using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
      /*  private List<Item> Similar(AlsoTry Record)
        {
            var records = _context.AlsoTry.Where(a => !a.IsActive);
            var history = Record.Getval(Record.S_Phrase);
            var index = new List<Item>();
                foreach(var r in history)
            {
                index.AddRange((IEnumerable<Item>)(from a in records
                               where a.S_Phrase.Contains(r)
                               select a.Transaction.GetCart()));
                    
            }
            return index.GroupBy(a => a.id);
           /* var other = from a in records
        }                join b in history
                        on a.S_Phrase */
            //var LikeOther = 

        private String Priority(AlsoTry profile)
        {
            var prices = profile.Budget().Count;
            var searches = profile.Getval(profile.S_Phrase).Count;
            return prices > searches ? "price" : "search";
        }
         
        //Get: Recommendation
        public async Task<IActionResult> Recomended()
        {

            var id = int.Parse(HttpContext.User.FindFirst(x => x.Type == ClaimTypes.SerialNumber)?.Value);
            var myrecord = _context.AlsoTry.Where(i => i.registeredUsers.id == id && i.IsActive).First();
            var bought = _context.AlsoTry.Where(i => !i.IsActive).Include(p=>p.Transaction.Cart).ToList();
            var rating = _context.Item.Where(i => i.Rating > 0).OrderByDescending(i => i.Rating).ToList();
            var myPhrases = myrecord.Getval(myrecord.S_Phrase);
          //  var similarse = myrecord.Getval(myrecord.S_Phrase).ForEach(i => bought.ForEach(f => f.S_Phrase.Contains(i)));
            bought = bought.Where(i=>i.Similarity(myPhrases)>0).OrderByDescending(x => x.Similarity(myPhrases)).Take(3).ToList();
            var boughtList = bought.Select(x => x.Transaction.Cart).Distinct();
            var recomended = new List<Item>() ;
            foreach (var lis in boughtList)
                recomended.AddRange(lis);
            recomended.AddRange(rating.Take(4 - recomended.Count()));
            return View(recomended);


            /*var similar = bought.Join(myrecord,
                b => b.Transaction.GetCart(),
                m=> m.
                )
             var similar = from t in _context.AlsoTry
                           join b in bought 
                           on t.Getval(t.S_Phrase).Intersect()
             
            var similar = from b in bought
                          join t in myrecord */
            return View(await _context.AlsoTry.ToListAsync());
        }

        // GET: AlsoTries
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlsoTry.ToListAsync());
        }
/*
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
        }*/
    }
}

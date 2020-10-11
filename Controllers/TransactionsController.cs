using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace projectWEB.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly projectWEBContext _context;

        public TransactionsController(projectWEBContext context)
        {
            _context = context;
        }
        public void ValidStatistics()
        {
            var Collector = _context;
        }
        
        public async Task<IActionResult> CommitToBuy()
        {
            var Unpaid = _context.Transaction.Where(u => u.CustomerId.ToString() == HttpContext.Session.GetString("userId") && u.Status == 0).Include(p=>p.Cart).FirstOr(null);
            var Profile = _context.AlsoTry.Where(p => p.Transaction == Unpaid).First();
            Unpaid.Status = Status.Approved;
            Unpaid.TranscationDate = DateTime.Now;
            Profile.IsActive = false;
            try
            {
                _context.Update(Profile);
                _context.Update(Unpaid);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(Unpaid.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            foreach(Item it in Unpaid.Cart)
            {
                it.amount -= 1;
                _context.Item.Update(it);
            }
            await _context.SaveChangesAsync();
            return View();
        }

        
        public async Task<IActionResult> Addtocart( int? id)
        {
          
            var Unpaid = _context.Transaction.Where(u => u.CustomerId.ToString() == HttpContext.Session.GetString("userId")&&u.Status==0).FirstOr(null);
           // var chosenitem = _context.Item.Where(i => i.id == Int32.Parse(ItemId)).First();
                       var chosenitem = _context.Item.Where(i => i.id ==id).First();

            Transaction transaction = new Transaction() { CustomerId = Int32.Parse(HttpContext.Session.GetString("userId")),SumPrice=0 };
        
            if (ModelState.IsValid)
            {
                
                if (Unpaid != null)
                {   if (Unpaid.Cart.Where(u => u.id == chosenitem.id).Count() > 0) { return StatusCode(204); };
                    Unpaid.AddCart(chosenitem);
                    _context.Update(Unpaid);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                     transaction.AddCart(chosenitem);
                      _context.Add(transaction);
                    await _context.SaveChangesAsync();
                }

            }

            return View(transaction);
        }

        public  IActionResult MyCart()
        {                                                          
            var Unpaid = _context.Transaction.Where(u => u.Customer.id.ToString() == HttpContext.Session.GetString("userId") && u.Status == 0).Include(p => p.Cart);
            if (Unpaid.Count() == 0)
            {
                return View(null);
            }
            ViewBag.Total = Unpaid.FirstOr(null).SumPrice;
            var thiscart = Unpaid.First().Cart;
            return View(thiscart);//.GetCart());
            
        }
        public async Task<bool> rmvfrmcart(int id)
        {
            var UsrCart = _context.Transaction.Where(u => u.Customer.id.ToString() == HttpContext.Session.GetString("userId") && u.Status == 0).First();//.Include(p => p.Cart);
            var ItemRmv = _context.Item.Where(i => i.id == id).First();
            if (UsrCart.Cart.Contains(ItemRmv)) 
            { 
                UsrCart.Cart.Remove(ItemRmv);
                _context.Update(UsrCart);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
                // GET: Transactions
          public async Task<IActionResult> Index()
           {
            var cart = _context.Transaction.Where(t => t.Customer.id.ToString() == HttpContext.Session.GetString("userId")).Include(p => p.Cart);
            return View(cart);

           }
        /*
         * 06/10 before changes
         * 
         *         public  IActionResult MyCart()
        {
            var Unpaid = _context.Transaction.Where(u => u.CustomerId.ToString() == HttpContext.Session.GetString("userId") && u.Status == 0).FirstOr(null);
            if (Unpaid == null)
            {
                return View(null);
            }
            ViewBag.Total = Unpaid.SumPrice;
            return View(Unpaid.GetCart());

        }

         * 
         * 
         * 
         *         public async Task<IActionResult> Index()
                {
                    var projectWEBContext = _context.Transaction.Include(t => t.Customer);
                    return View(await projectWEBContext.ToListAsync());
                }
                // GET: Transactions/Details/5
                public async Task<IActionResult> Details(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var transaction = await _context.Transaction
                        .Include(t => t.Customer)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (transaction == null)
                    {
                        return NotFound();
                    }

                    return View(transaction);
                }
        
                // GET: Transactions/Create
                public IActionResult Create()
                {
                    ViewData["CustomerId"] = new SelectList(_context.RegisteredUsers, "id", "Email");
                    return View();
                }

                // POST: Transactions/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("Id,TranscationDate,SumPrice,Status,CustomerId")] Transaction transaction)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(transaction);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["CustomerId"] = new SelectList(_context.RegisteredUsers, "id", "Email", transaction.CustomerId);
                    return View(transaction);
                }

                // GET: Transactions/Edit/5
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var transaction = await _context.Transaction.FindAsync(id);
                    if (transaction == null)
                    {
                        return NotFound();
                    }
                    ViewData["CustomerId"] = new SelectList(_context.RegisteredUsers, "id", "Email", transaction.CustomerId);
                    return View(transaction);
                }

                // POST: Transactions/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to, for 
                // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("Id,TranscationDate,SumPrice,Status,CustomerId")] Transaction transaction)
                {
                    if (id != transaction.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(transaction);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!TransactionExists(transaction.Id))
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
                    ViewData["CustomerId"] = new SelectList(_context.RegisteredUsers, "id", "Email", transaction.CustomerId);
                    return View(transaction);
                }

                // GET: Transactions/Delete/5
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var transaction = await _context.Transaction
                        .Include(t => t.Customer)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (transaction == null)
                    {
                        return NotFound();
                    }

                    return View(transaction);
                }

                // POST: Transactions/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    var transaction = await _context.Transaction.FindAsync(id);
                    _context.Transaction.Remove(transaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
*/
                private bool TransactionExists(int id)
                {
                    return _context.Transaction.Any(e => e.Id == id);
                }
    }

}

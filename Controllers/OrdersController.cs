using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class OrdersController : Controller
    {
        private readonly projectWEBContext _context;

        public OrdersController(projectWEBContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            //var query = from o in _context.Order
            //            join ru in _context.RegisteredUsers on o.user_id equals ru.id into oru
            //            from x in oru.DefaultIfEmpty()
            //            select new
            //            {
            //                UserName = x.UserName,
            //                UsergroupName = u.UsergroupName,
            //                Price = (x == null ? String.Empty : x.Price)
            //            };

            var result = _context.Order.Where(o => o.user_id == int.Parse(HttpContext.Session.GetInt32("userId").ToString()))
                             .Select(order => order.date.Date)
                             .Distinct()
                             .OrderByDescending(order => order.Date);
            ViewBag.History = result.ToList();
            return View("History"); 
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(DateTime? date)
        {
            if (date == null)
            {
                return NotFound();
            }

            //var order = _context.Order.Where(o => o.date.Date == date);
            var details = (from o in _context.Order
                           join i in _context.Item on o.item_id equals i.id
                           join c in _context.Category on i.ItemDevision equals c.id.ToString()
                           where o.date.Date == date
                           select new MyDetails 
                           {
                               name = i.ItemName,
                               quantity = o.item_quantity,
                               price = i.price,
                               category = c.name
                           }).ToList();

            if (details == null)
            {
                return NotFound();
            }
            ViewBag.Details = details;
            return View();
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,item_id,item_quantity,date,user_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,item_id,item_quantity,date,user_id")] Order order)
        {
            if (id != order.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using projectWEB.Data;
using projectWEB.Models;

namespace projectWEB.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(projectWEBContext context) : base(context)
        {
        }

        // GET: Orders
        public async Task<IActionResult> ListAllOrdersByDate()
        {
            var result = _context.Order.Where(o => o.user_id == int.Parse(HttpContext.Session.GetInt32("userId").ToString()))
                             .Select(order => order.date.Date)
                             .Distinct()
                             .OrderByDescending(order => order.Date);
            ViewBag.History = result.ToList();
            return View("History"); 
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> SearchOneOrderByDate(DateTime? date)
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
                           select new OrderDetails
                           {
                               order_id = o.id,
                               item_name = i.ItemName,
                               item_quantity = o.item_quantity,
                               item_price = i.price,
                               item_category = c.name
                           }).ToList();

            if (details == null)
            {
                return NotFound();
            }
            ViewBag.Details = details;
            return View("Details");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var claims = HttpContext.User.Claims;

            var value = HttpContext.Session.GetString("cart");
            Dictionary<int, ItemInCart> cart = JsonConvert.DeserializeObject<Dictionary<int, ItemInCart>>(value);
            var itemsInCart = cart.Values.ToList();
            Order[] orders = new Order[itemsInCart.Count];
            for (int i = 0; i < itemsInCart.Count; i++)
            {
                Order ord = new Order
                {
                    item_id = itemsInCart[i].id,
                    item_quantity = itemsInCart[i].quantity,
                    user_id = int.Parse(HttpContext.Session.GetInt32("userId").ToString()),
                    date = DateTime.Now,
                };
                orders[i] = ord;
            }
            _context.Order.AddRange(orders);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _context.SaveChanges();
            }
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Mainshop", "Items");
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

        // UpdateQuantity
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
                    var orderr = await _context.Order.FirstOrDefaultAsync(o => o.id == id);
                    orderr.item_quantity = order.item_quantity;
                    _context.Update(orderr);
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
            }
            return RedirectToAction("ListAllOrdersByDate", "Orders");
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
            return RedirectToAction("ListAllOrdersByDate", "Orders");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.id == id);
        }
    }
}

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
                             .GroupBy(o => new { o.order_number, o.date.Date })
                             .Select(order => order.Key  )
                             .OrderByDescending(order => order.Date);    
            ViewBag.History = result.ToList();
            return View("History"); 
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> SearchByOrderNumber(int order_number)
        {
            if (order_number == null)
            {
                return NotFound();
            }

            //var order = _context.Order.Where(o => o.date.Date == date);
            var details = (from o in _context.Order
                           join i in _context.Item on o.item_id equals i.id
                           join c in _context.Category on i.ItemDevision equals c.id.ToString()
                           where o.order_number == order_number
                           select new OrderDetails
                           {
                               order_number = o.order_number,
                               item_name = i.ItemName,
                               item_quantity = o.item_quantity,
                               item_price = i.price,
                               item_category = c.name,
                               order_id = o.id,
                               order_date = o.date
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
            //var claims = HttpContext.User.Claims;
            Random RandNum = new Random();
            int order_number = RandNum.Next(1000000, 9999999);
            IQueryable<Order> tmp = null;
            tmp = _context.Order.Where(o => o.order_number == order_number);
            while (tmp != null && tmp.Count() != 0)
            {
                order_number = RandNum.Next(1000000, 9999999);
                tmp = _context.Order.Where(o => o.order_number == order_number);
            }
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
                    order_number = order_number,
                };
                orders[i] = ord;
                subtractItemQuantity(itemsInCart[i].id, itemsInCart[i].quantity);
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

        // Subtract the orderAmount from the overall amount
        private void subtractItemQuantity(int id, int orderAmount)
        {
            var items = _context.Item.Where(u => u.id == id);
            Item item = items.FirstOrDefault();
            item.amount -= orderAmount;
            _context.Item.Update(item);
            _context.SaveChanges();
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projectWEB.Data;
using projectWEB.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace projectWEB.Controllers
{
    public class CartController : BaseController
    {
        public CartController(projectWEBContext context) : base(context)
        {
        }

        // GET: Cart
        public ActionResult Index()
        {
            setCategoriesMenu();
            return View();
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Add(int id, string cartView)
        {
            try
            {
                var items = _context.Item.Where(u => u.id == id);
                Item item = items.FirstOrDefault();
                ISession session = HttpContext.Session;
                if (HttpContext.Session.GetString("cart") == null)
                {
                    Dictionary<int, ItemInCart> cart = new Dictionary<int, ItemInCart>();
                    cart.Add(id, new ItemInCart(id, item.ItemName, item.price, 1));
                    session.SetString("cart", JsonConvert.SerializeObject(cart));
                }
                else
                {
                    var value = session.GetString("cart");
                    Dictionary<int, ItemInCart> cart = JsonConvert.DeserializeObject<Dictionary<int, ItemInCart>>(value);
                    if (cart.ContainsKey(id))
                    {
                        int quantity = cart[id].quantity + 1;
                        cart.Remove(id);
                        cart.Add(id, new ItemInCart(id, item.ItemName, item.price, quantity));
                    }
                    else
                    {
                        cart.Add(id, new ItemInCart(id, item.ItemName, item.price, 1));
                    }
                    session.SetString("cart", JsonConvert.SerializeObject(cart));
                }
                var val = HttpContext.Session.GetString("cart");
                Dictionary<int, ItemInCart> myCart = JsonConvert.DeserializeObject<Dictionary<int, ItemInCart>>(val);

                int size = 0;
                double total = 0;

                List<ItemInCart> values = myCart.Values.ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    size += values[i].quantity;
                    total += values[i].quantity * values[i].price;
                }

                session.SetInt32("size", size);
                session.SetString("total", total.ToString());
                if (cartView != null)
                {
                    return RedirectToAction("Index", "Cart");
                }
                return RedirectToAction("Mainshop", "Items");
            }
            catch (Exception ex)
            {
                return View("~/Views/Items/mainshop.cshtml");
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public string GetCart()
        {
            return HttpContext.Session.GetString("cart");
        }

        [Authorize]
        public void Checkout()
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
        }
    }
}
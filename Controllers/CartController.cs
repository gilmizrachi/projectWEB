using System;
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
using Microsoft.AspNetCore.Identity;
using projectWEB.ViewModels;
using projectWEB.Helpers;
using projectWEB.Extensions;

namespace projectWEB.Controllers
{
    public class CartController : Controller
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        private readonly SignInManager<RegisteredUsers> _signInManager;
        public CartController(projectWEBContext context, UserManager<RegisteredUsers> userManager,
                    SignInManager<RegisteredUsers> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Cart
        public ActionResult Index()
        {
            //setCategoriesMenu();
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
                var items = _context.ProductRecords.Where(u => u.ID == id);
                ProductRecord item = items.FirstOrDefault();
                ISession session = HttpContext.Session;
                if (HttpContext.Session.GetString("cart") == null)
                {
                    Dictionary<int, CartItem> cart = new Dictionary<int, CartItem>();
                    //cart.Add(id, new CartItem(id, item.Name, item,, 1));
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
                        //cart.Add(id, new ItemInCart(id, item.ProductName, item.price, quantity));
                    }
                    else
                    {
                        //cart.Add(id, new ItemInCart(id, item.ProductName, item.price, 1));
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
        public IActionResult Delete(int id, IFormCollection collection)
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
        public IActionResult Cart(bool isPopup = false)
        {
            CartItemsViewModel model = new CartItemsViewModel();

            if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0)
            {
                model.CartItems = SessionHelper.CartItems.OrderByDescending(x => x.ItemID).ToList();
                model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.Products = GetProductsByIDs(model.ProductIDs);
                }
            }

            if (!string.IsNullOrEmpty(SessionHelper.PromoCode))
            {
                var promo = GetPromoByCode(SessionHelper.PromoCode);

                if (promo != null && promo.Value > 0 && (promo.ValidTill == null || promo.ValidTill >= DateTime.Now))
                {
                    model.PromoCode = promo.Code;
                    model.Promo = promo;
                }
            }

            if (isPopup)
            {
                return PartialView("_CartPopup", model);
            }
            else
            {
                bool isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

                if (isAjaxCall)
                {
                    return PartialView("_CartItems", model);
                }
                else return View("CartItems", model);
            }
        }
        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            return IDs.Select(id => _context.Products.Find(id)).Where(x => !x.IsDeleted && !x.Category.IsDeleted).OrderBy(x => x.ID).ToList();
        }
        public Promo GetPromoByCode(string code)
        {
            return _context.Promos.FirstOrDefault(x => !x.IsDeleted && x.Code == code);
        }
        public IActionResult UpdateCart(IFormCollection formCollection)
        {
            var cartItemsUpdate = GetCartItemUpdateFromForm(formCollection);

            CartItemsViewModel model = new CartItemsViewModel();

            if (SessionHelper.CartItems != null)
            {
                var productIDs = cartItemsUpdate.CartItems != null &&
                                   cartItemsUpdate.CartItems.Count > 0 ?
                                   cartItemsUpdate.CartItems.Select(x => x.ItemID).ToList() : new List<int>();

                if (productIDs.Count > 0)
                {
                    model.Products = GetProductsByIDs(productIDs);
                }

                SessionHelper.CartItems.Clear();

                if (model.Products != null && model.Products.Count > 0)
                {
                    foreach (var product in model.Products)
                    {
                        var productPrice = product.Discount.HasValue && product.Discount.Value > 0 ? product.Discount.Value : product.Price;

                        SessionHelper.CartItems.Add(new CartItem() { ItemID = product.ID, Price = productPrice, Quantity = cartItemsUpdate.CartItems.FirstOrDefault(x => x.ItemID == product.ID).Quantity });
                    }
                }

                model.CartItems = SessionHelper.CartItems.OrderByDescending(x => x.ItemID).ToList();
                model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();
            }

            if (!string.IsNullOrEmpty(cartItemsUpdate.PromoCode))
            {
                var promo = GetPromoByCode(cartItemsUpdate.PromoCode);

                if (promo != null && promo.Value > 0 && (promo.ValidTill == null || promo.ValidTill >= DateTime.Now))
                {
                    SessionHelper.Promo = promo;
                    SessionHelper.PromoCode = promo.Code;

                    model.Promo = promo;
                }

                model.PromoCode = cartItemsUpdate.PromoCode;
            }

            return PartialView("_CartItems", model);
        }
        private UpdateCartItemsViewModel GetCartItemUpdateFromForm(IFormCollection formCollection)
        {
            var model = new UpdateCartItemsViewModel
            {
                CartItems = new List<CartItem>()
            };

            foreach (var item in formCollection)
            {
                var key = formCollection[item.ToString()];

                if (key.Contains("product_"))
                {
                    var value = formCollection[key];

                    if (!string.IsNullOrEmpty(value))
                    {
                        var cartItem = new CartItem
                        {
                            ItemID = int.TryParse(key.ToString().GetSubstringText("_", ""), out int productID) ? productID : 0,

                            Quantity = int.TryParse(value, out int quantity) ? quantity : 0
                        };

                        model.CartItems.Add(cartItem);
                    }
                }
                else if (key.Contains("promo"))
                {
                    var value = formCollection[key];

                    if (!string.IsNullOrEmpty(value))
                    {
                        model.PromoCode = value;
                    }
                }
            }

            return model;
        }
        public JsonResult AddItemToCart(int itemID, int quantity = 1)
        {
            var product = GetProductByID(itemID);

            if (product != null)
            {
                var message = string.Empty;

                var itemInCart = SessionHelper.CartItems.FirstOrDefault(x => x.ItemID == product.ID);

                if (itemInCart != null)
                {
                    //update cart item quantity.
                    itemInCart.Quantity += quantity;
                    message = "PP.Shopping.CartItemQuantityUpdated";
                }
                else
                {
                    var productPrice = product.Discount.HasValue && product.Discount.Value > 0 ? product.Discount.Value : product.Price;

                    //add the product to cart.
                    SessionHelper.CartItems.Add(new CartItem() { ItemID = product.ID, Price = productPrice, Quantity = quantity });
                    message = "PP.Shopping.ItemAddedToCart";
                }

                return new JsonResult( new { Success = true, Message = message, CartItems = SessionHelper.CartItems });
            }
            else
            {
                return new JsonResult( new { Success = false, Message = "PP.Shopping.ItemNotFound" });
            }
        }

        public JsonResult GetCartItems()
        {
            return new JsonResult(new {
                Data = new { Success = true, CartItems = SessionHelper.CartItems }
            });

        }

        public async Task<IActionResult> CheckoutAsync()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0)
            {
                model.CartItems = SessionHelper.CartItems.OrderByDescending(x => x.ItemID).ToList();
                model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();

                if (model.ProductIDs.Count > 0)
                {
                    model.Products = GetProductsByIDs(model.ProductIDs);
                }

                model.TotalAmount = SessionHelper.CartItems.Sum(z => z.ProductTotal);
            }

            if (!string.IsNullOrEmpty(SessionHelper.PromoCode))
            {
                var promo = GetPromoByCode(SessionHelper.PromoCode);

                if (promo != null && promo.Value > 0 && (promo.ValidTill == null || promo.ValidTill >= DateTime.Now))
                {
                    model.PromoCode = promo.Code;
                    model.Promo = promo;

                    if (model.Promo.PromoType == (int)PromoTypes.Percentage)
                    {
                        model.Discount = Math.Round((model.TotalAmount * model.Promo.Value) / 100);
                    }
                    else if (model.Promo.PromoType == (int)PromoTypes.Amount)
                    {
                        model.Discount = SessionHelper.Promo.Value;
                    }

                    model.PromoApplied = true;
                }
            }

            model.CartHasProducts = model.Products != null && model.Products.Count > 0;

            if (model.CartHasProducts)
            {
                model.FinalAmount = model.TotalAmount - model.Discount + ConfigurationsHelper.FlatDeliveryCharges;
            }

            if (User.Identity.IsAuthenticated)
            {
                model.User = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            }
            else
            {
                model.User = new RegisteredUsers();
            }
            bool isAjaxCall = HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                return PartialView("_Checkout", model);
            }
            else
            {
                return View(model);
            }
        }
        public Product GetProductByID(int ID)
        {
            var product = _context.Products.Include("Category.CategoryRecords").Include("ProductPictures.Picture").FirstOrDefault(x => x.ID == ID);

            return product != null && !product.IsDeleted && !product.Category.IsDeleted ? product : null;
        }
        //public string GetCart()
        //{
        //    return HttpContext.Session.GetString("cart");
        //}

        //[Authorize]
        //public void Checkout()
        //{
        //    var claims = HttpContext.User.Claims;

        //    var value = HttpContext.Session.GetString("cart");
        //    Dictionary<int, ItemInCart> cart = JsonConvert.DeserializeObject<Dictionary<int, ItemInCart>>(value);
        //    var itemsInCart = cart.Values.ToList();
        //    Order[] orders = new Order[itemsInCart.Count];
        //    for (int i = 0; i < itemsInCart.Count; i++)
        //    {
        //        //Order ord = new Order
        //        //{
        //        //    item_id = itemsInCart[i].id,
        //        //    item_quantity = itemsInCart[i].quantity,
        //        //    user_id = int.Parse(HttpContext.Session.GetInt32("userId").ToString()),
        //        //    date = DateTime.Now,
        //        //};
        //        //orders[i] = ord;
        //    }
        //    //_context.Order.AddRange(orders);

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        _context.SaveChanges();
        //    }
        //}
    }
}
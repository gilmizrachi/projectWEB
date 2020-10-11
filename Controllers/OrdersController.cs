using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Helpers;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using projectWEB.ViewModels;

namespace projectWEB.Controllers
{
    public class OrdersController : Controller
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        private readonly SignInManager<RegisteredUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public OrdersController(projectWEBContext context, UserManager<RegisteredUsers> userManager,
                    SignInManager<RegisteredUsers> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<JsonResult> PlaceOrder(PlaceOrderCrediCardViewModel model)
        {
            var errorDetails = string.Empty;

            if (model != null && ModelState.IsValid)
            {
                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0)
                {
                    model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();

                    if (model.ProductIDs.Count > 0)
                    {
                        model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs);
                    }
                }

                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0 && model.Products != null && model.Products.Count > 0)
                {
                    var newOrder = new Order();
                    if (User.Identity.IsAuthenticated)
                    {
                        newOrder.CustomerID = _userManager.GetUserId(User);
                    }
                    else if (model.CreateAccount)
                    {
                        try
                        {
                            var user = new RegisteredUsers { FullName = model.FullName, UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, RegisteredOn = DateTime.Now };
                            var result = await _userManager.CreateAsync(user);

                            if (result.Succeeded)
                            {
                                if (await _roleManager.RoleExistsAsync("User"))
                                {
                                    //assign User role to newly registered user
                                    await _userManager.AddToRoleAsync(user, "User");
                                }

                                await _signInManager.SignInAsync(user, isPersistent: false);

                                newOrder.CustomerID = user.Id;
                            }
                            else
                            {
                                return new JsonResult(new
                                {
                                    Success = false,
                                    Message = string.Join("<br />", result.Errors)
                                }) ;
                            }
                        }
                        catch
                        {
                            return new JsonResult(new
                            {
                                Success = false,
                                Message = string.Format("An error occured while registering user.")
                            });
                        }
                    }
                    else
                    {
                        newOrder.IsGuestOrder = true;
                    }

                    newOrder.CustomerName = model.FullName;
                    newOrder.CustomerEmail = model.Email;
                    newOrder.CustomerPhone = model.PhoneNumber;
                    newOrder.CustomerCountry = model.Country;
                    newOrder.CustomerCity = model.City;
                    newOrder.CustomerAddress = model.Address;
                    newOrder.CustomerZipCode = model.ZipCode;

                    newOrder.OrderItems = new List<OrderItem>();
                    foreach (var product in SessionHelper.CartItems.Select(x => model.Products.FirstOrDefault(y => y.ID == x.ItemID)))
                    {
                        var currentLanguageProductRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == 1);

                        var orderItem = new OrderItem
                        {
                            ProductID = product.ID,
                            ProductName = currentLanguageProductRecord != null ? currentLanguageProductRecord.Name : string.Format("Product ID : {0}", product.ID),
                            ItemPrice = product.Price,
                            Quantity = SessionHelper.CartItems.FirstOrDefault(x => x.ItemID == product.ID).Quantity
                        };

                        newOrder.OrderItems.Add(orderItem);
                    }

                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.TotalAmmount = newOrder.OrderItems.Sum(x => (x.ItemPrice * x.Quantity));
                    newOrder.DeliveryCharges = ConfigurationsHelper.FlatDeliveryCharges;

                    //Applying Promo/voucher
                    if (!string.IsNullOrEmpty(SessionHelper.PromoCode))
                    {
                        var promo = SessionHelper.Promo;
                        if (promo != null && promo.Value > 0)
                        {
                            if (promo.ValidTill == null || promo.ValidTill >= DateTime.Now)
                            {
                                newOrder.PromoID = promo.ID;

                                if (promo.PromoType == (int)PromoTypes.Percentage)
                                {
                                    newOrder.Discount = Math.Round((newOrder.TotalAmmount * promo.Value) / 100);
                                }
                                else if (promo.PromoType == (int)PromoTypes.Amount)
                                {
                                    newOrder.Discount = promo.Value;
                                }
                            }
                        }
                    }

                    newOrder.FinalAmmount = newOrder.TotalAmmount + newOrder.DeliveryCharges - newOrder.Discount;
                    newOrder.PaymentMethod = (int)PaymentMethods.CreditCard;

                    newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Placed,
                            ModifiedOn = DateTime.Now,
                            Note = "Order Placed."
                        }
                    };

                    newOrder.PlacedOn = DateTime.Now;

                    if (true) //success
                    {
                        newOrder.TransactionID = "tempTransactionID";

                        if (SaveOrder(newOrder))
                        {
                            SessionHelper.ClearCart();

                            if (!newOrder.IsGuestOrder)
                            {
                                //send order placed notification email
                                //await _userManager.SendEmailAsync(newOrder.CustomerID, EmailTextHelpers.OrderPlacedEmailSubject(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Tracking", "Orders", new { orderID = newOrder.ID }, protocol: Request.Url.Scheme)));

                                ////send order placed notification email to admin emails
                                //await new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.AdminEmailAddress, EmailTextHelpers.OrderPlacedEmailSubject_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Details", "Orders", new { area = "Dashboard", ID = newOrder.ID }, protocol: Request.Url.Scheme)));
                            }

                            return new JsonResult( new
                            {
                                Success = true,
                                OrderID = newOrder.ID
                            });
                        }
                        else
                        {
                            return new JsonResult(new
                            {
                                Success = false,
                                Message = "System can not take any order."
                            });
                        }
                    }
                }
                else
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "Invalid products in cart."
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {
                    Success = false,
                    Message = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }
        }
        public async Task<JsonResult> PlaceOrderViaCashOnDelivery(PlaceOrderCashOnDeliveryViewModel model)
        {
            var errorDetails = string.Empty;

            if (model != null && ModelState.IsValid)
            {
                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0)
                {
                    model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();

                    if (model.ProductIDs.Count > 0)
                    {
                        model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs);
                    }
                }

                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0 && model.Products != null && model.Products.Count > 0)
                {
                    var newOrder = new Order();

                    if (User.Identity.IsAuthenticated)
                    {
                        newOrder.CustomerID = _userManager.GetUserId(User);
                    }
                    else if (model.CreateAccount)
                    {
                        try
                        {
                            var user = new RegisteredUsers { FullName = model.FullName, UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, RegisteredOn = DateTime.Now };

                            var result = await _userManager.CreateAsync(user);

                            if (result.Succeeded)
                            {
                                if (await _roleManager.RoleExistsAsync("User"))
                                {
                                    //assign User role to newly registered user
                                    await _userManager.AddToRoleAsync(user, "User");
                                }

                                await _signInManager.SignInAsync(user, isPersistent: false);

                                newOrder.CustomerID = user.Id;
                            }
                            else
                            {
                                return new JsonResult(new
                                {
                                    Success = false,
                                    Message = string.Join("<br />", result.Errors)
                                });
                            }
                        }
                        catch
                        {
                            return new JsonResult(new 
                            {
                                Success = false,
                                Message = string.Format("An error occured while registering user.")
                            });
                        }
                    }
                    else
                    {
                        newOrder.IsGuestOrder = true;
                    }

                    newOrder.CustomerName = model.FullName;
                    newOrder.CustomerEmail = model.Email;
                    newOrder.CustomerPhone = model.PhoneNumber;
                    newOrder.CustomerCountry = model.Country;
                    newOrder.CustomerCity = model.City;
                    newOrder.CustomerAddress = model.Address;
                    newOrder.CustomerZipCode = model.ZipCode;

                    newOrder.OrderItems = new List<OrderItem>();
                    foreach (var product in SessionHelper.CartItems.Select(x => model.Products.FirstOrDefault(y => y.ID == x.ItemID)))
                    {
                        var currentLanguageProductRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == 1);

                        var orderItem = new OrderItem
                        {
                            ProductID = product.ID,
                            ProductName = currentLanguageProductRecord != null ? currentLanguageProductRecord.Name : string.Format("Product ID : {0}", product.ID),

                            ItemPrice = product.Discount.HasValue && product.Discount.Value > 0 ? product.Discount.Value : product.Price,

                            Quantity = SessionHelper.CartItems.FirstOrDefault(x => x.ItemID == product.ID).Quantity
                        };

                        newOrder.OrderItems.Add(orderItem);
                    }

                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.TotalAmmount = newOrder.OrderItems.Sum(x => (x.ItemPrice * x.Quantity));
                    newOrder.DeliveryCharges = ConfigurationsHelper.FlatDeliveryCharges;

                    //Applying Promo/voucher 
                    if (!string.IsNullOrEmpty(SessionHelper.PromoCode))
                    {
                        var promo = SessionHelper.Promo;
                        if (promo != null && promo.Value > 0)
                        {
                            if (promo.ValidTill == null || promo.ValidTill >= DateTime.Now)
                            {
                                newOrder.PromoID = promo.ID;

                                if (promo.PromoType == (int)PromoTypes.Percentage)
                                {
                                    newOrder.Discount = Math.Round((newOrder.TotalAmmount * promo.Value) / 100);
                                }
                                else if (promo.PromoType == (int)PromoTypes.Amount)
                                {
                                    newOrder.Discount = promo.Value;
                                }
                            }
                        }
                    }

                    newOrder.FinalAmmount = newOrder.TotalAmmount + newOrder.DeliveryCharges - newOrder.Discount;
                    newOrder.PaymentMethod = (int)PaymentMethods.CashOnDelivery;

                    newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Placed,
                            ModifiedOn = DateTime.Now,
                            Note = "Order Placed."
                        }
                    };

                    newOrder.PlacedOn = DateTime.Now;

                    if (SaveOrder(newOrder))
                    {
                        SessionHelper.ClearCart();

                        if (!newOrder.IsGuestOrder)
                        {
                            //send order placed notification email
                            //await UserManager.SendEmailAsync(newOrder.CustomerID, EmailTextHelpers.OrderPlacedEmailSubject(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Tracking", "Orders", new { orderID = newOrder.ID }, protocol: Request.Url.Scheme)));

                            ////send order placed notification email to admin emails
                            //await new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.AdminEmailAddress, EmailTextHelpers.OrderPlacedEmailSubject_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Details", "Orders", new { area = "Dashboard", ID = newOrder.ID }, protocol: Request.Url.Scheme)));
                        }

                        return new JsonResult( new
                        {
                            Success = true,
                            OrderID = newOrder.ID
                        });
                    }
                    else
                    {
                        return new JsonResult(new
                        {
                            Success = false,
                            Message = "System can not take any order."
                        });
                    }
                }
                else
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "Invalid products in cart."
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {
                    Success = false,
                    Message = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }
        }
        public async Task<JsonResult> PlaceOrderViaPayPal(PlaceOrderPayPalViewModel model)
        {
            var errorDetails = string.Empty;

            if (model != null && ModelState.IsValid)
            {
                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0)
                {
                    model.ProductIDs = SessionHelper.CartItems.Select(x => x.ItemID).ToList();

                    if (model.ProductIDs.Count > 0)
                    {
                        model.Products = ProductsService.Instance.GetProductsByIDs(model.ProductIDs);
                    }
                }

                if (SessionHelper.CartItems != null && SessionHelper.CartItems.Count > 0 && model.Products != null && model.Products.Count > 0)
                {
                    var newOrder = new Order();

                    if (User.Identity.IsAuthenticated)
                    {
                        newOrder.CustomerID = _userManager.GetUserId(User);
                    }
                    else if (model.CreateAccount)
                    {
                        try
                        {
                            var user = new RegisteredUsers { FullName = model.FullName, UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, RegisteredOn = DateTime.Now };

                            var result = await _userManager.CreateAsync(user);

                            if (result.Succeeded)
                            {
                                if (await _roleManager.RoleExistsAsync("User"))
                                {
                                    //assign User role to newly registered user
                                    await _userManager.AddToRoleAsync(user, "User");
                                }

                                await _signInManager.SignInAsync(user, isPersistent: false);

                                newOrder.CustomerID = user.Id;
                            }
                            else
                            {
                                return new JsonResult(new
                                {
                                    Success = false,
                                    Message = string.Join("<br />", result.Errors)
                                });
                            }
                        }
                        catch
                        {
                            return new JsonResult(new
                            {
                                Success = false,
                                Message = string.Format("An error occured while registering user.")
                            });
                        }
                    }
                    else
                    {
                        newOrder.IsGuestOrder = true;
                    }

                    newOrder.CustomerName = model.FullName;
                    newOrder.CustomerEmail = model.Email;
                    newOrder.CustomerPhone = model.PhoneNumber;
                    newOrder.CustomerCountry = model.Country;
                    newOrder.CustomerCity = model.City;
                    newOrder.CustomerAddress = model.Address;
                    newOrder.CustomerZipCode = model.ZipCode;

                    newOrder.OrderItems = new List<OrderItem>();
                    foreach (var product in SessionHelper.CartItems.Select(x => model.Products.FirstOrDefault(y => y.ID == x.ItemID)))
                    {
                        var currentLanguageProductRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == 1);

                        var orderItem = new OrderItem
                        {
                            ProductID = product.ID,
                            ProductName = currentLanguageProductRecord != null ? currentLanguageProductRecord.Name : string.Format("Product ID : {0}", product.ID),
                            ItemPrice = product.Price,
                            Quantity = SessionHelper.CartItems.FirstOrDefault(x => x.ItemID == product.ID).Quantity
                        };

                        newOrder.OrderItems.Add(orderItem);
                    }

                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.TotalAmmount = newOrder.OrderItems.Sum(x => (x.ItemPrice * x.Quantity));
                    newOrder.DeliveryCharges = ConfigurationsHelper.FlatDeliveryCharges;

                    //Applying Promo/voucher 
                    if (!string.IsNullOrEmpty(SessionHelper.PromoCode))
                    {
                        var promo = SessionHelper.Promo;
                        if (promo != null && promo.Value > 0)
                        {
                            if (promo.ValidTill == null || promo.ValidTill >= DateTime.Now)
                            {
                                newOrder.PromoID = promo.ID;

                                if (promo.PromoType == (int)PromoTypes.Percentage)
                                {
                                    newOrder.Discount = Math.Round((newOrder.TotalAmmount * promo.Value) / 100);
                                }
                                else if (promo.PromoType == (int)PromoTypes.Amount)
                                {
                                    newOrder.Discount = promo.Value;
                                }
                            }
                        }
                    }

                    newOrder.FinalAmmount = newOrder.TotalAmmount + newOrder.DeliveryCharges - newOrder.Discount;
                    newOrder.PaymentMethod = (int)PaymentMethods.PayPal;
                    newOrder.TransactionID = model.TransactionID;

                    newOrder.OrderHistory = new List<OrderHistory>() {
                        new OrderHistory() {
                            OrderStatus = (int)OrderStatus.Placed,
                            ModifiedOn = DateTime.Now,
                            Note = string.Format("Order placed via PayPal by PayPal Account Name: {0} ({1})", model.AccountName, model.AccountEmail)
                        }
                    };

                    newOrder.PlacedOn = DateTime.Now;

                    if (SaveOrder(newOrder))
                    {
                        SessionHelper.ClearCart();

                        if (!newOrder.IsGuestOrder)
                        {
                            //send order placed notification email
                            //await UserManager.SendEmailAsync(newOrder.CustomerID, EmailTextHelpers.OrderPlacedEmailSubject(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Tracking", "Orders", new { orderID = newOrder.ID }, protocol: Request.Url.Scheme)));

                            //send order placed notification email to admin emails
                            //await new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.AdminEmailAddress, EmailTextHelpers.OrderPlacedEmailSubject_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID), EmailTextHelpers.OrderPlacedEmailBody_Admin(AppDataHelper.CurrentLanguage.ID, newOrder.ID, Url.Action("Details", "Orders", new { area = "Dashboard", ID = newOrder.ID }, protocol: Request.Url.Scheme)));
                        }

                        return new JsonResult(new
                        {
                            Success = true,
                            OrderID = newOrder.ID
                        });
                    }
                    else
                    {
                        return new JsonResult(new
                        {
                            Success = false,
                            Message = "System can not take any order."
                        });
                    }
                }
                else
                {
                    return new JsonResult(new
                    {
                        Success = false,
                        Message = "Invalid products in cart."
                    });
                }
            }
            else
            {
                return new JsonResult(new
                {
                    Success = false,
                    Message = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))
                });
            }
        }

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

            var result = _context.Orders.Where(o => o.CustomerID == (HttpContext.Session.GetInt32("userId").ToString()))
                             .Select(order => order.PlacedOn.Date)
                             .Distinct()
                             .OrderByDescending(order => order.Date);
            ViewBag.History = result.ToList();
            return View("History");
        }
        public IActionResult PrintInvoice(int orderID)
        {
            PrintInvoiceViewModel model = new PrintInvoiceViewModel
            {
                OrderID = orderID,

                Order = GetOrderByID(orderID)
            };

            if (model.Order == null)
            {
                return NotFound();
            }

            return PartialView("_PrintInvoice", model);
        }
        public Order GetOrderByID(int ID)
        {
            return _context.Orders.Include("OrderItems.Product.ProductRecords").FirstOrDefault(x => x.ID == ID);
        }
        public IActionResult Tracking(int? orderID, bool orderPlaced = false)
        {
            TrackOrderViewModel model = new TrackOrderViewModel
            {
                OrderID = orderID
            };

            if (orderID.HasValue)
            {
                model.Order = GetOrderByID(orderID.Value);
            }

            ViewBag.ShowOrderPlaceMessage = orderPlaced;

            return View(model);
        }
        public List<Order> SearchOrders(string userID, int? orderID, int? orderStatus, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var orders = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(userID))
            {
                orders = orders.Where(x => x.CustomerID.Equals(userID));
            }

            if (orderID.HasValue && orderID.Value > 0)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            count = orders.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return orders.OrderByDescending(x => x.PlacedOn).Skip(skipCount).Take(recordSize).ToList();
        }
        public async Task<IActionResult> Details(DateTime? date)
        {
            if (date == null)
            {
                return NotFound();
            }

            //var order = _context.Order.Where(o => o.date.Date == date);
            var details = (from o in _context.Orders
                           join i in _context.ProductRecords on o.ID equals i.ID
                           join c in _context.Categories on i.ID equals c.ID
                           where o.PlacedOn.Date == date
                           select new MyDetails
                           {
                               name = i.Name,
                               quantity = o.OrderItems.Count,
                               price = i.ID,// change it to price later,
                               category = c.SanitizedName
                           }).ToList();

            if (details == null)
            {
                return NotFound();
            }
            ViewBag.Details = details;
            return View();
        }
        public bool SaveOrder(Order order)
        {
            _context.Orders.Add(order);

            return _context.SaveChanges() > 0;
        }
    }
}

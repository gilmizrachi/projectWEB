using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using projectWEB.Controllers;
using projectWEB.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;

namespace projectWEB.Helpers
{
    public static class URLHelper
    {
        public static string Home(this IUrlHelper helper)
        {
            return helper.Action("Index", "Home");
        }

        public static string StaticPage(this IUrlHelper helper, string pageid)
        {

            string routeURL = string.Empty;
            routeURL = helper.Action(string.Format("{0}", pageid));

            return routeURL;
        }

        public static string SubscribeNewsLetter(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("SubscribeNewsLetter");

            return routeURL;
        }

        public static string SubmitContactForm(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("SubmitContactForm");

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string Register(this IUrlHelper helper)
        {
            string url = helper.Action("Register", "RegisteredUsers");

            return url;
        }

        public static string Login(this IUrlHelper helper, string returnUrl = "")
        {
            var routeValues = new RouteValueDictionary();

            if(!string.IsNullOrEmpty(returnUrl))
            {
                routeValues.Add("returnUrl", returnUrl);
            }
            return helper.Action("Login", "RegisteredUsers", routeValues);
            
        }

        public static string ForgotPassword(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("ForgotPassword");

            return routeURL;
        }

        public static string ResetPassword(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("ResetPassword");

            return routeURL;
        }

        public static string Logoff(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

           routeURL = helper.Action("Logoff");

            return routeURL;
        }

        public static string SearchProducts(this IUrlHelper helper, string category = "", string q = "", decimal? from = 0.0M, decimal? to = 0.0M, string sortby = "", int? pageNo = 0, int? recordSize = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("category", category);

            if (!string.IsNullOrEmpty(q))
            {
                routeValues.Add("q", q);
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                routeValues.Add("from", from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                routeValues.Add("to", to.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                routeValues.Add("sortby", sortby);
            }

            if (recordSize.HasValue && recordSize.Value > 1 && recordSize.Value != (int)RecordSizeEnums.Size20)
            {
                routeValues.Add("recordSize", recordSize.Value);
            }
            
            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }
            
            routeURL = helper.Action("Search","Home", routeValues);
            return routeURL;
        }

        public static string ProductDetails(this IUrlHelper helper, string category, int ID, string sanitizedtitle = "")
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("category", category);
            routeValues.Add("ID", ID);

            if(!string.IsNullOrEmpty(sanitizedtitle))
            {
                routeValues.Add("sanitizedtitle", sanitizedtitle);
            }

            routeURL = helper.Action("ProductDetails", routeValues);

            return routeURL;
        }

        public static string UserProfile(this IUrlHelper helper, string tab = "")
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            if(!string.IsNullOrEmpty(tab))
            {
                routeValues.Add("tab", tab);
            }

            routeURL = helper.Action("UserProfile", routeValues);

            return routeURL;
        }
        public static string UpdateProfile(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

           routeURL = helper.Action("UpdateProfile");

            return routeURL;
        }

        public static string ChangePassword(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("ChangePassword");

            return routeURL;
        }
        public static string UpdatePassword(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("UpdatePassword");

            return routeURL;
        }

        public static string ChangeAvatar(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("ChangeAvatar");

            return routeURL;
        }
        public static string UpdateAvatar(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("UpdateAvatar");

            return routeURL;
        }
        
        public static string UserOrders(this IUrlHelper helper, string userID = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            if (!string.IsNullOrEmpty(userID))
            {
                routeValues.Add("userID", userID);
            }

            if (orderID.HasValue && orderID.Value > 0)
            {
                routeValues.Add("orderID", orderID.Value);
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                routeValues.Add("orderStatus", orderStatus.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }
            
            routeURL = helper.Action("UserOrders", routeValues);

            return routeURL;
        }

        public static string Cart(this IUrlHelper helper, bool isPopup = false)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            if (isPopup)
            {
                routeValues.Add("isPopup", isPopup);
            }

            routeURL = helper.Action("Cart", routeValues);

            return routeURL;
        }
        public static string UpdateCart(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

           routeURL = helper.Action("UpdateCart");

            return routeURL;
        }
        public static string AddItemToCart(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("AddItemToCart");

            return routeURL;
        }
        public static string GetCartItems(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("GetCartItems");

            return routeURL;
        }
        public static string CartProducts(this IUrlHelper helper)
        {
            string routeURL = string.Empty;
            
           routeURL = helper.Action("CartProducts");

            return routeURL.ToLower();
        }
        public static string CartItems(this IUrlHelper helper)
        {
            string routeURL = string.Empty;
            
            routeURL = helper.Action("CartItems");

            return routeURL;
        }
        public static string Checkout(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("Checkout");

            return routeURL;
        }

        public static string PlaceOrder(this IUrlHelper helper, bool isCashOnDelivery = false, bool isPayPal = false)
        {
            string routeURL = string.Empty;
            var LanguageBased = string.Empty;

            if (isCashOnDelivery)
            {
                routeURL = helper.RouteUrl(string.Format("{0}PlaceOrderViaCashOnDelivery", LanguageBased));
            }
            else if (isPayPal)
            {
                routeURL = helper.RouteUrl(string.Format("{0}PlaceOrderViaPayPal", LanguageBased));
            }
            else
            {
                routeURL = helper.RouteUrl(string.Format("{0}PlaceOrder", LanguageBased));
            }

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string OrderTrack(this IUrlHelper helper, string orderID = "", bool orderPlaced = false)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();
            
            if (!string.IsNullOrEmpty(orderID))
            {
                routeValues.Add("orderID", orderID);
            }

            if (orderPlaced)
            {
                routeValues.Add("orderPlaced", orderPlaced);
            }

            routeURL = helper.Action("OrderTrack", routeValues);

            return routeURL;
        }

        public static string PrintInvoice(this IUrlHelper helper, int orderID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("orderID", orderID);
            
           routeURL = helper.Action("PrintInvoice", routeValues);

            return routeURL;
        }

        public static string UploadPictures(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

           routeURL = helper.Action("UploadPictures");

            return routeURL;
        }

        public static string UploadPicturesWithoutDatabase(this IUrlHelper helper, bool isSiteFolder = false, string subFolder = "")
        {
            string routeURL = string.Empty;
            var routeValues = new RouteValueDictionary();
            
            if (isSiteFolder)
            {
                routeValues.Add("isSiteFolder", isSiteFolder);
            }

            if (!string.IsNullOrEmpty(subFolder))
            {
                routeValues.Add("subFolder", subFolder);
            }

            routeURL = helper.Action("UploadPicturesWithoutDatabase", routeValues);

            return routeURL;
        }

        public static string LeaveReview(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

           routeURL = helper.Action("LeaveReview");

            return routeURL;
        }

        public static string DeleteReview(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("DeleteReview");

            return routeURL;
        }
        public static string UserReviews(this IUrlHelper helper, string userID = "", string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Reviews");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (!string.IsNullOrEmpty(userID))
            {
                routeValues.Add("userID", userID);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.Action("UserReviews", routeValues);

            return routeURL;
        }

        public static string ChangeMode(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.Action("ChangeMode");

            return routeURL;
        }
    }
}
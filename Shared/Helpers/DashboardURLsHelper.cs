using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace projectWEB.Helpers
{
    public static class DashboardURLsHelper
    {
        public static string Dashboard(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Dashboard");

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string ListAction(this IUrlHelper helper, string controller, string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        
        public static string Categories(this IUrlHelper helper, string searchTerm = "", int? pageNo = 0, int? parentCategoryID = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Categories");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
            {
                routeValues.Add("parentCategoryID", parentCategoryID.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string Products(this IUrlHelper helper, string searchTerm = "", int? pageNo = 0, int? categoryID = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Products");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                routeValues.Add("categoryID", categoryID.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        public static string Promos(this IUrlHelper helper, string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Promos");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }
            
            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        
        public static string Orders(this IUrlHelper helper, string userID = "", int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Orders");

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

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string UserOrdersList(this IUrlHelper helper, string userID, int? orderID = 0, int? orderStatus = 0, int? pageNo = 0)
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

            routeURL = helper.RouteUrl("UserOrdersList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        public static string Languages(this IUrlHelper helper, string searchTerm = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string UpdateOrderStatus(this IUrlHelper helper, int orderID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("orderID", orderID);
            
            routeURL = helper.RouteUrl("UpdateOrderStatus", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string Users(this IUrlHelper helper, string searchTerm = "", string roleID = "", int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Users");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                routeValues.Add("roleID", roleID);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }
            
            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string Comments(this IUrlHelper helper, string searchTerm = "", string userID = "", int? pageNo = 0, bool showUserCommentsOnly = false)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Comments");

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

            if (showUserCommentsOnly)
            {
                routeValues.Add("showUserCommentsOnly", showUserCommentsOnly);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string Configurations(this IUrlHelper helper, string searchTerm = "", int? configurationType = 0, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Configurations");

            if (!string.IsNullOrEmpty(searchTerm))
            {
                routeValues.Add("searchTerm", searchTerm);
            }

            if (configurationType.HasValue && configurationType.Value > 0)
            {
                routeValues.Add("configurationType", configurationType.Value);
            }

            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("EntityList", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string CreateAction(this IUrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);

            routeURL = helper.RouteUrl("EntityCreate", routeValues);
            
            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string EditAction(this IUrlHelper helper, string controller, object ID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);
            routeValues.Add("ID", ID);

            routeURL = helper.RouteUrl("EntityEdit", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string DetailsAction(this IUrlHelper helper, string controller, object ID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);
            routeValues.Add("ID", ID);

            routeURL = helper.RouteUrl("EntityDetails", routeValues);
            
            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string EditAction(this IUrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);

            routeURL = helper.RouteUrl("EntityEditWithoutID", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string DeleteAction(this IUrlHelper helper, string controller)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", controller);

            routeURL = helper.RouteUrl("EntityDelete", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        public static string DisableLanguage(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");
            routeValues.Add("disable", true);

           routeURL = helper.RouteUrl("ChangeLanguageStatus", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }
        public static string EnableLanguage(this IUrlHelper helper)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");
            routeValues.Add("disable", false);

            routeURL = helper.RouteUrl("ChangeLanguageStatus", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string LanguageResources(this IUrlHelper helper, int ID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");
            routeValues.Add("Action", "Resources");
            routeValues.Add("ID", ID);

            routeURL = helper.RouteUrl("LanguageResources", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string ImportResources(this IUrlHelper helper, int ID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");
            routeValues.Add("Action", "ImportResources");
            routeValues.Add("ID", ID);

            routeURL = helper.RouteUrl("ImportLanguageResources", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string ExportResources(this IUrlHelper helper, int ID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("Controller", "Languages");
            routeValues.Add("Action", "ExportResources");
            routeValues.Add("ID", ID);

            routeURL = helper.RouteUrl("ExportLanguageResources", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string UserDetails(this IUrlHelper helper, string userID)
        {
            string routeURL = string.Empty;
            var routeValues = new RouteValueDictionary();

            routeValues.Add("userID", userID);

            routeURL = helper.RouteUrl("UserDetails", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string UserRoles(this IUrlHelper helper, string userID)
        {
            string routeURL = string.Empty;
            var routeValues = new RouteValueDictionary();

            routeValues.Add("userID", userID);

            routeURL = helper.RouteUrl("UserRoles", routeValues);
            
            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string RoleDetails(this IUrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;
            var routeValues = new RouteValueDictionary();

            routeValues.Add("roleID", roleID);

            routeURL = helper.RouteUrl("RoleDetails", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string RoleUsers(this IUrlHelper helper, string roleID, int? pageNo = 0)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("roleID", roleID);
            
            if (pageNo.HasValue && pageNo.Value > 1)
            {
                routeValues.Add("pageNo", pageNo.Value);
            }

            routeURL = helper.RouteUrl("RoleUsers", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string AssignUserRole(this IUrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            var routeValues = new RouteValueDictionary();

            routeValues.Add("userID", userID);

            routeURL = helper.RouteUrl("AssignUserRole", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

        public static string RemoveUserRole(this IUrlHelper helper, string userID)
        {
            string routeURL = string.Empty;
            var routeValues = new RouteValueDictionary();

            routeValues.Add("userID", userID);

            routeURL = helper.RouteUrl("RemoveUserRole", routeValues);

            routeURL = WebUtility.UrlDecode(routeURL);
            return routeURL.ToLower();
        }

    }
}
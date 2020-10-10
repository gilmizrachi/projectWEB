using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectWEB.Helpers
{
    public static class HTMLHelper
    {
        public static IHtmlContent MenuItemClass(this IHtmlHelper htmlHelper, string controllerName, string actionName = "")
        {
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString().ToLower();

            if (String.Equals(controllerName, currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                if(!string.IsNullOrEmpty(actionName))
                {
                    var currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString().ToLower();

                    if (String.Equals(actionName, currentAction, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return new HtmlString("active");
                    }
                    else return new HtmlString("");
                }
                else return new HtmlString("active");
            }
            else
                return new HtmlString("");
        }

        public static string getCellBackgroundClassByOrderStatus(this IHtmlHelper htmlHelper, OrderStatus orderStatus)
        {
            var bgClass = string.Empty;

            switch (orderStatus)
            {
                case OrderStatus.Placed:
                    bgClass = "bg-primary text-white";
                    break;
                case OrderStatus.Processing:
                case OrderStatus.WaitingForPayment:
                    bgClass = "bg-info text-white";
                    break;
                case OrderStatus.Delivered:
                    bgClass = "bg-success text-white";
                    break;
                case OrderStatus.Failed:
                case OrderStatus.Cancelled:
                    bgClass = "bg-danger text-white";
                    break;
                case OrderStatus.OnHold:
                case OrderStatus.Refunded:
                    bgClass = "bg-warning";
                    break;
                default:
                    bgClass = string.Empty;
                    break;
            }

            return bgClass;
        }
        public static string getCellBackgroundClassByLanguageStatus(this IHtmlHelper htmlHelper, bool enabled)
        {
            var bgClass = string.Empty;

            if(!enabled)
            {
                bgClass = "bg-danger text-white";
            }
            else
            {
                bgClass = "bg-success text-white";
            }
            
            return bgClass;
        }

        public static string GetFontAwesomeIconForSocialMediaProvider(this IHtmlHelper htmlHelper, string socialMediaProvider)
        {
            var fontAwesomeClass = string.Empty;

            switch (socialMediaProvider)
            {
                case "Facebook":
                    fontAwesomeClass = "fab fa-facebook-f";
                    break;
                case "Twitter":
                    fontAwesomeClass = "fab fa-twitter";
                    break;
                case "Google":
                    fontAwesomeClass = "fab fa-google";
                    break;
                case "Microsoft":
                    fontAwesomeClass = "fab fa-microsoft";
                    break;
                default:
                    fontAwesomeClass = string.Empty;
                    break;
            }

            return fontAwesomeClass;
        }
        public static string GetButtonBackgroundClassForSocialMediaProvider(this IHtmlHelper htmlHelper, string socialMediaProvider)
        {
            var fontAwesomeClass = string.Empty;

            switch (socialMediaProvider)
            {
                case "Facebook":
                    fontAwesomeClass = "bg-primary";
                    break;
                case "Twitter":
                    fontAwesomeClass = "bg-info";
                    break;
                case "Google":
                    fontAwesomeClass = "bg-danger";
                    break;
                case "Microsoft":
                    fontAwesomeClass = "bg-success";
                    break;
                default:
                    fontAwesomeClass = string.Empty;
                    break;
            }

            return fontAwesomeClass;
        }
    }
}
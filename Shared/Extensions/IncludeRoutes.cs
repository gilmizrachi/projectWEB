using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace projectWEB.Extensions
{
    public static class IncludeRoutes
    {
        public static void UseMvcWithRoutes(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Register",
                    template: "register",
                    defaults: new { area = "", controller = "RegisteredUsers", action = "Register" }
                );

                routes.MapRoute(
                    name: "Login",
                    template: "login",
                    defaults: new { area = "", controller = "RegisteredUsers", action = "Login" }
                );
                routes.MapRoute(
                    name: "ForgotPassword",
                    template: "forgot-password",
                    defaults: new { area = "", controller = "RegisteredUsers", action = "ForgotPassword" }
                );
                routes.MapRoute(
                    name: "ResetPassword",
                    template: "reset-password",
                    defaults: new { area = "", controller = "RegisteredUsers", action = "ResetPassword" }
                );

                routes.MapRoute(
                    name: "Logoff",
                    template: "logoff",
                    defaults: new { area = "", controller = "Users", action = "LogOff" }
                );

                routes.MapRoute(
                    name: "SearchProducts",
                    template: "search/{category}",
                    defaults: new { area = "", controller = "Home", action = "Search", category = UrlParameter.Optional }

                );


                routes.MapRoute(
                    name: "ProductDetails",
                    template: "{category}/product/{ID}/{sanitizedtitle}",
                    defaults: new { area = "", controller = "Products", action = "Details", sanitizedtitle = UrlParameter.Optional }

                );


                routes.MapRoute(
                    name: "UserProfile",
                    template: "user/profile",
                    defaults: new { area = "", controller = "Users", action = "UserProfile" }

                );


                routes.MapRoute(
                    name: "UpdateProfile",
                    template: "user/update-profile",
                    defaults: new { area = "", controller = "Users", action = "UpdateProfile" }

                );


                routes.MapRoute(
                    name: "ChangePassword",
                    template: "user/change-password",
                    defaults: new { area = "", controller = "Users", action = "ChangePassword" }

                );


                routes.MapRoute(
                    name: "UpdatePassword",
                    template: "user/update-password",
                    defaults: new { area = "", controller = "Users", action = "UpdatePassword" }

                );


                routes.MapRoute(
                    name: "ChangeAvatar",
                    template: "user/change-avatar",
                    defaults: new { area = "", controller = "Users", action = "ChangeAvatar" }

                );


                routes.MapRoute(
                    name: "UpdateAvatar",
                    template: "user/update-avatar",
                    defaults: new { area = "", controller = "Users", action = "UpdateAvatar" }

                );


                routes.MapRoute(
                    name: "UserOrders",
                    template: "user/orders",
                    defaults: new { controller = "Orders", action = "UserOrders" }

                );



                routes.MapRoute(
                    name: "Cart",
                    template: "cart",
                    defaults: new { area = "", controller = "Cart", action = "Cart" }

                );

                routes.MapRoute(
                    name: "UpdateCart",
                    template: "update-cart",
                    defaults: new { area = "", controller = "Cart", action = "UpdateCart" }

                );


                routes.MapRoute(
                    name: "AddItemToCart",
                    template: "add-to-cart",
                    defaults: new { area = "", controller = "Cart", action = "AddItemToCart" }

                );


                routes.MapRoute(
                    name: "GetCartItems",
                    template: "get-cart-items",
                    defaults: new { area = "", controller = "Cart", action = "GetCartItems" }

                );


                routes.MapRoute(
                    name: "Checkout",
                    template: "checkout",
                    defaults: new { area = "", controller = "Cart", action = "Checkout" }

                );


                routes.MapRoute(
                    name: "DeliveryInfo",
                    template: "delivery-info",
                    defaults: new { area = "", controller = "Cart", action = "DeliveryInfo" }

                );


                routes.MapRoute(
                    name: "ConfirmOrder",
                    template: "confirm-order",
                    defaults: new { area = "", controller = "Cart", action = "ConfirmOrder" }

                );


                routes.MapRoute(
                    name: "PlaceOrder",
                    template: "place-order",
                    defaults: new { area = "", controller = "Cart", action = "PlaceOrder" }

                );


                routes.MapRoute(
                    name: "PlaceOrderViaCashOnDelivery",
                    template: "place-order-cod",
                    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaCashOnDelivery" }

                );


                routes.MapRoute(
                    name: "PlaceOrderViaPayPal",
                    template: "place-order-paypal",
                    defaults: new { area = "", controller = "Orders", action = "PlaceOrderViaPayPal" }

                );


                routes.MapRoute(
                    name: "OrderTrack",
                    template: "tracking",
                    defaults: new { area = "", controller = "Orders", action = "Tracking" }

                );


                routes.MapRoute(
                    name: "PrintInvoice",
                    template: "print-ivoice",
                    defaults: new { area = "", controller = "Orders", action = "PrintInvoice" }

                );



                routes.MapRoute(
                    name: "FeaturedProducts",
                    template: "featured-products",
                    defaults: new { area = "", controller = "Products", action = "FeaturedProducts" }

                );


                routes.MapRoute(
                    name: "RecentProducts",
                    template: "recent-products",
                    defaults: new { area = "", controller = "Products", action = "RecentProducts" }

                );



                routes.MapRoute(
                    name: "RelatedProducts",
                    template: "related-products",
                    defaults: new { area = "", controller = "Products", action = "RelatedProducts" }

                );



                routes.MapRoute(
                    name: "CategoriesMenu",
                    template: "categories-menu",
                    defaults: new { area = "", controller = "Categories", action = "CategoriesMenu" }

                );



                routes.MapRoute(
                    name: "UploadPictures",
                    template: "pictures/upload",
                    defaults: new { area = "", controller = "Shared", action = "UploadPictures" }

                );



                routes.MapRoute(
                    name: "UploadPicturesWithoutDatabase",
                    template: "pictures/upload-nodb/{subFolder}",
                    defaults: new { area = "", controller = "Shared", action = "UploadPicturesWithoutDatabase", subFolder = UrlParameter.Optional }

                );



                routes.MapRoute(
                    name: "LeaveComment",
                    template: "add-comment",
                    defaults: new { area = "", controller = "Comments", action = "LeaveComment" }

                );


                routes.MapRoute(
                    name: "DeleteComment",
                    template: "delete-comment",
                    defaults: new { area = "", controller = "Comments", action = "DeleteComment" }

                );


                routes.MapRoute(
                    name: "UserComments",
                    template: "user/comments",
                    defaults: new { area = "", controller = "Comments", action = "UserComments" }

                );


                routes.MapRoute(
                    name: "SubmitContactForm",
                    template: "contact-form-submit",
                    defaults: new { area = "", controller = "Home", action = "SubmitContactForm" }

                );


                routes.MapRoute(
                    name: "Home",
                    template: "",
                    defaults: new { controller = "Home", action = "Index" }

                );


                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
            });
        }
    }
}

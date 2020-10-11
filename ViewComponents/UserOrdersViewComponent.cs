using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Controllers;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class UserOrdersViewComponent : ViewComponent
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        public UserOrdersViewComponent(projectWEBContext context, UserManager<RegisteredUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke(string userID, int? orderID, int? orderStatus, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            UserOrdersViewModel model = new UserOrdersViewModel
            {
                UserID = !string.IsNullOrEmpty(userID) ? userID : _userManager.GetUserId(UserClaimsPrincipal),
                OrderID = orderID,
                OrderStatus = orderStatus
            };

            model.UserOrders = SearchOrders(model.UserID, model.OrderID, model.OrderStatus, pageNo, pageSize, count: out int ordersCount);

            model.Pager = new Pager(ordersCount, pageNo, pageSize);

            return View("_UserOrders", model);
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

    }
}

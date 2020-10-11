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
    public class UserReviewsViewComponent : ViewComponent
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        public UserReviewsViewComponent(projectWEBContext context, UserManager<RegisteredUsers> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userID, string searchTerm, int? pageNo = 1, int entityID = 1, bool isPartial = false)
        {
            REviewsListingViewModel model = new REviewsListingViewModel
            {
                SearchTerm = searchTerm
            };

            if (!string.IsNullOrEmpty(userID))
            {
                model.User = await _userManager.FindByIdAsync(userID);
            }
            else
            {
                model.User = await _userManager.FindByIdAsync(_userManager.GetUserId(UserClaimsPrincipal));
            }

            model.Reviews = SearchReviews(entityID: entityID, recordID: null, userID: model.User.Id, searchTerm: model.SearchTerm, pageNo: pageNo, recordSize: (int)RecordSizeEnums.Size10, count: out int reviewsCount);

            if (model.Reviews != null && model.Reviews.Count > 0)
            {
                var productIDs = model.Reviews.Select(x => x.RecordID).ToList();

                model.ReviewedProducts = GetProductsByIDs(productIDs);
            }

            model.Pager = new Pager(reviewsCount, pageNo, (int)RecordSizeEnums.Size10);

            if (isPartial)
            {
                return View("_UserReviewsListing", model);
            }
            else
            {
                return View("_UserReviews", model);
            }
        }
        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            //var products = _context.Products.Include(x => x.ProductPictures).Include(p => p.ProductRecords).ThenInclude(ps => ps.ProductSpecifications).ToList();
            return IDs.Select(id => _context.Products.Find(id)).Where(x => !x.IsDeleted && !x.Category.IsDeleted).OrderBy(x => x.ID).ToList();
        }
        public List<Reviews> SearchReviews(int? entityID, int? recordID, string userID, string searchTerm, int? pageNo, int recordSize, out int count)
        {

            var reviews = _context.Reviews
                                  .Where(x => !x.IsDeleted)
                                  .AsQueryable();

            if (entityID.HasValue && entityID.Value > 0)
            {
                reviews = reviews.Where(x => x.EntityID == entityID.Value);
            }

            if (recordID.HasValue && recordID.Value > 0)
            {
                reviews = reviews.Where(x => x.RecordID == recordID.Value);
            }

            if (!string.IsNullOrEmpty(userID))
            {
                reviews = reviews.Where(x => x.UserID == userID);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reviews = reviews.Where(x => x.Text.ToLower().Contains(searchTerm.ToLower()));
            }

            count = reviews.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return reviews.Include("User")
                           .Include("User.Picture")
                           .OrderByDescending(x => x.TimeStamp)
                           .Skip(skipCount)
                           .Take(recordSize)
                           .ToList();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Controllers;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.ViewModels
{
    public class ProductReviewsViewComponent : ViewComponent
    {
        private readonly projectWEBContext _context;
        public ProductReviewsViewComponent(projectWEBContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int productID, int pageNo = 1, int recordSize = 10)
        {
            EntityReviewsViewModel model = new EntityReviewsViewModel
            {
                EntityID = 1,
                RecordID = productID
            };

            model.Reviews = SearchReviews(entityID: model.EntityID, recordID: model.RecordID, userID: null, searchTerm: null, pageNo: pageNo, recordSize: recordSize, out int count);

            return View("_ProductReviews", model);
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

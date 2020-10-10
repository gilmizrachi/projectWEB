using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Shared.Enums;
using projectWEB.ViewModels;

namespace projectWEB.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly projectWEBContext _context;
        private readonly UserManager<RegisteredUsers> _userManager;
        private readonly SignInManager<RegisteredUsers> _signInManager; public ReviewsController(projectWEBContext context, UserManager<RegisteredUsers> userManager,
                    SignInManager<RegisteredUsers> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LeaveReview(ReviewsViewModel model)
        {
            try
            {
                var review = new Reviews
                {
                    Text = model.Text,
                    Rating = model.Rating,
                    EntityID = model.EntityID,
                    RecordID = model.RecordID,
                    UserID = _userManager.GetUserId(User),
                    TimeStamp = DateTime.Now,

                    LanguageID = 1
                };

                var res = AddReview(review);

                return new JsonResult( new { Success = res });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false, Message = ex.Message });
            }
        }
        public bool AddReview(Reviews review)
        {
            _context.Reviews.Add(review);

            return _context.SaveChanges() > 0;
        }
        public IActionResult ProductReviews(int productID, int pageNo = 1, int recordSize = 10)
        {
            EntityReviewsViewModel model = new EntityReviewsViewModel
            {
                EntityID = 1,
                RecordID = productID
            };

            model.Reviews = SearchReviews(entityID: model.EntityID, recordID: model.RecordID, userID: null, searchTerm: null, pageNo: pageNo, recordSize: recordSize, out int count);

            return PartialView("_ProductReviews", model);
        }

        public async Task<IActionResult> UserReviews(string userID, string searchTerm, int? pageNo = 1, int entityID = 1, bool isPartial = false)
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
                model.User = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
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
                return PartialView("_UserReviewsListing", model);
            }
            else
            {
                return PartialView("_UserReviews", model);
            }
        }
        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            return IDs.Select(id => _context.Products.Find(id)).Where(x => !x.IsDeleted && !x.Category.IsDeleted).OrderBy(x => x.ID).ToList();
        }
        [HttpPost]
        public JsonResult DeleteReview(int ID)
        {

            try
            {
                var review = GetReviewByID(ID);

                if (review != null && User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || review.UserID == _userManager.GetUserId(User)))
                {
                    var operation = DeleteReview(review);

                    return new JsonResult( new { Success = operation, Message = operation ? string.Empty : "PP.ProductDetails.Reviews.Validations.UnableToDeleteReview" });
                }
                else
                {
                    throw new Exception("PP.ProductDetails.Reviews.Validations.NotAuthorizedToDeleteReview");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false, Message = string.Format("{0}", ex.Message) });
            }
        }
        public Reviews GetReviewByID(int ID)
        {

            var review = _context.Reviews.FirstOrDefault(x => x.ID == ID);

            return review != null && !review.IsDeleted ? review : null;
        }

        public bool DeleteReview(Reviews review)
        {
            review.IsDeleted = true;

            _context.Entry(review).State = EntityState.Modified;
            
            return _context.SaveChanges() > 0;
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

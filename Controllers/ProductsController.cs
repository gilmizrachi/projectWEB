using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;
using projectWEB.Services;
using projectWEB.Shared.Enums;
using projectWEB.ViewModels;

namespace projectWEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly projectWEBContext _context;
        public IActionResult FeaturedProducts(int? productID, int pageSize = (int)RecordSizeEnums.Size10, bool isForHomePage = false)
        {
            FeaturedProductsViewModel model = new FeaturedProductsViewModel
            {
                Products = ProductsService.Instance.SearchFeaturedProducts(recordSize: pageSize, excludeProductIDs: new List<int>() { productID.HasValue ? productID.Value : 0 })
            };

            if (isForHomePage)
            {
                return PartialView("_FeaturedProductsHomePage", model);
            }
            else
            {
                return PartialView("_FeaturedProducts", model);
            }
        }
        public IActionResult RelatedProducts(int categoryID, int recordSize = (int)RecordSizeEnums.Size6)
        {
            RelatedProductsViewModel model = new RelatedProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(new List<int>() { categoryID }, null, null, null, null, 1, recordSize, out int count)
            };

            if (model.Products == null || model.Products.Count < (int)RecordSizeEnums.Size6)
            {
                //the realted products are less than the specfified RelatedProductsRecordsSize, so instead show featured products
                model.Products = ProductsService.Instance.SearchFeaturedProducts(recordSize);
                model.IsFeaturedProductsOnly = true;
            }

            return PartialView("_RelatedProducts", model);
        }
        public ProductsController(projectWEBContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var projectWEBContext = _context.Products.Include(p => p.Category);
            return View(await projectWEBContext.ToListAsync());
        }
        [HttpGet]
        public IActionResult Details(int ID, string category)
        {
            ProductDetailsViewModel model = new ProductDetailsViewModel
            {
                Product = ProductsService.Instance.GetProductByID(ID)
            };

            if (model.Product == null || !model.Product.Category.SanitizedName.ToLower().Equals(category))
                return NotFound();

            var productRating = new ProductRating();

            var productComments = _context.Reviews.Where(x => !x.IsDeleted && x.EntityID == 1 && x.RecordID == model.Product.ID);

            productRating.TotalRatings = productComments.Count();
            productRating.AverageRating = productRating.TotalRatings > 0 ? (int)productComments.Average(x => x.Rating) : 0;

            model.Rating = productRating;

            return View(model);
        }
        // GET: Products/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,Price,Discount,Cost,isFeatured,ThumbnailPictureID,SKU,Tags,Barcode,Supplier,ID,IsActive,IsDeleted,ModifiedOn")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "name", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "name", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Price,Discount,Cost,isFeatured,ThumbnailPictureID,SKU,Tags,Barcode,Supplier,ID,IsActive,IsDeleted,ModifiedOn")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "name", product.CategoryID);
            return View(product);
        }
        public IActionResult RecentProducts(int? productID, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                pageSize = (int)RecordSizeEnums.Size10;
            }

            FeaturedProductsViewModel model = new FeaturedProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(null, null, null, null, null, 1, pageSize, out int count)
            };

            return PartialView("_RecentProducts", model);
        }
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using projectWEB.Data;
using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace projectWEB.Services
{
    public class ProductsService
    {
        #region Define as Singleton
        private static ProductsService _Instance;

        public static ProductsService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ProductsService();
                }

                return (_Instance);
            }
        }

        private ProductsService()
        {
        }
        #endregion

        public List<Product> GetAllProducts(int? pageNo = 1, int? recordSize = 0)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var products = context.Products
                                    .Where(x => !x.IsDeleted && !x.Category.IsDeleted)
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    products = products.Skip(skip)
                                       .Take(recordSize.Value);
                }

                return products.ToList();
            }
        }

        public List<Product> SearchFeaturedProducts(int? pageNo = 1, int? recordSize = 0, List<int> excludeProductIDs = null)
        {
            excludeProductIDs = excludeProductIDs ?? new List<int>();

            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var products = context.Products
                                    .Where(x => !x.IsDeleted && !x.Category.IsDeleted && x.isFeatured && !excludeProductIDs.Contains(x.ID))
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    products = products.Skip(skip)
                                           .Take(recordSize.Value);
                }

                return products.ToList();
            }
        }

        public List<Product> SearchProducts(List<int> categoryIDs, string searchTerm, decimal? from, decimal? to, string sortby, int? pageNo, int recordSize, out int count)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var products = context.Products
                                  .Where(x => !x.IsDeleted && !x.Category.IsDeleted)
                                  .AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = context.ProductRecords
                                      .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                      .Select(x => x.Product)
                                      .AsQueryable();
                }

                if (categoryIDs != null && categoryIDs.Count > 0)
                {
                    products = products.Where(x => categoryIDs.Contains(x.CategoryID));
                }

                if (from.HasValue && from.Value > 0.0M)
                {
                    products = products.Where(x => x.Price >= from.Value);
                }

                if (to.HasValue && to.Value > 0.0M)
                {
                    products = products.Where(x => x.Price <= to.Value);
                }

                if (!string.IsNullOrEmpty(sortby))
                {
                    if (string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                    {
                        products = products.OrderByDescending(x => x.Price);
                    }
                    else
                    {
                        products = products.OrderBy(x => x.Price);
                    }
                }
                else //sortBy Product Date
                {
                    products = products.OrderByDescending(x => x.ModifiedOn);
                }

                count = products.Count();

                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * recordSize;

                return products.Skip(skipCount).Take(recordSize).Include("Category.CategoryRecords").Include("ProductPictures.Picture").ToList();
            }
        }

        public Product GetProductByID(int ID)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var product = context.Products.Include("Category.CategoryRecords").Include("ProductPictures.Picture").FirstOrDefault(x => x.ID == ID);

                return product != null && !product.IsDeleted && !product.Category.IsDeleted ? product : null;
            }
        }
        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                return IDs.Select(id => context.Products.Find(id)).Where(x => !x.IsDeleted && !x.Category.IsDeleted).OrderBy(x => x.ID).ToList();
            }
        }

        public ProductRecord GetProductRecordByID(int ID)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var productRecord = context.ProductRecords.Find(ID);

                return productRecord != null && !productRecord.IsDeleted ? productRecord : null;
            }
        }
        public decimal GetMaxProductPrice()
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var products = context.Products.Where(x => !x.IsDeleted && !x.Category.IsDeleted);

                return products.Count() > 0 ? products.Max(x => x.Price) : 0;
            }
        }

        public bool SaveProduct(Product product)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                context.Products.Add(product);

                return context.SaveChanges() > 0;
            }
        }

        public bool SaveProductRecord(ProductRecord productRecord)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                context.ProductRecords.Add(productRecord);

                return context.SaveChanges() > 0;
            }
        }

        public bool UpdateProduct(Product product)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var existingProduct = context.Products.Find(product.ID);

                context.Entry(existingProduct).CurrentValues.SetValues(product);

                return context.SaveChanges() > 0;
            }
        }

        public bool UpdateProductPictures(int productID, List<ProductPicture> newPictures)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var oldPictures = context.ProductPictures.Where(p => p.ProductID == productID);

                context.ProductPictures.RemoveRange(oldPictures);

                context.ProductPictures.AddRange(newPictures);

                return context.SaveChanges() > 0;
            }
        }
        public bool UpdateProductRecord(ProductRecord productRecord)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var existingRecord = context.ProductRecords.Find(productRecord.ID);

                context.Entry(existingRecord).CurrentValues.SetValues(productRecord);

                return context.SaveChanges() > 0;
            }
        }
        public void UpdateProductSpecifications(int productRecordID, List<ProductSpecification> newProductSpecification)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var oldProductSpecifications = context.ProductSpecifications.Where(p => p.ProductRecordID == productRecordID);

                var taskPro = Task.Factory.StartNew(() =>
                  {
                      context.ProductSpecifications.RemoveRange(oldProductSpecifications);
                      return context.SaveChangesAsync();
                  }, TaskCreationOptions.LongRunning);
                if (taskPro.IsCompleted)
                {
                    context.ProductSpecifications.AddRange(newProductSpecification);
                }
                context.SaveChangesAsync();
            }
        }
        public bool DeleteProduct(int ID)
        {
            var options = DataContextHelper.GetNewContext();
            using (var context = new projectWEBContext(options))
            {
                var product = context.Products.Find(ID);

                product.IsDeleted = true;

                context.Entry(product).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }
    }
}

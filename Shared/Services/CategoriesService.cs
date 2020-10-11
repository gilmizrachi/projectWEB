using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using projectWEB.Data;
using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectWEB.Services
{
    public class CategoriesService
    {
        #region Define as Singleton
        private static CategoriesService _Instance;

        public static CategoriesService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CategoriesService();
                }

                return (_Instance);
            }
        }

        private CategoriesService()
        {
        }
        #endregion

        public List<Category> GetCategories(int? pageNo = 1, int? recordSize = 0)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categories = context.Categories
                                                .Where(x => !x.IsDeleted)
                                                .OrderBy(x => x.ID).Include(x => x.CategoryRecords).Include(p => p.Products)
                                                .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    categories = categories.Skip(skip)
                                           .Take(recordSize.Value);
                }

                return categories.ToList();
            }
            
        }

        public List<Category> GetFeaturedCategories(int? pageNo = 1, int? recordSize = 0, bool includeProducts = false)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categories = context.Categories
                                    .Where(x => !x.IsDeleted && x.isFeatured)
                                    .OrderBy(x => x.ID).Include(x => x.CategoryRecords).Include(p => p.Products).ThenInclude(pc=>pc.ProductRecords)
                                    .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    categories = categories.Skip(skip)
                                           .Take(recordSize.Value);
                }

                if (includeProducts)
                {
                    categories = categories.Include("Products.ProductRecords");
                }

                return categories.ToList();
            }
        }

        public List<CategoryRecord> GetCategoriesRecordsByCategory(int categoryID, int? pageNo = 1, int? recordSize = 0)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categoryRecords = context.CategoryRecords
                                         .Where(x => x.CategoryID == categoryID && !x.IsDeleted)
                                         .OrderBy(x => x.ID)
                                         .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    categoryRecords = categoryRecords.Skip(skip)
                                                     .Take(recordSize.Value);
                }

                return categoryRecords.ToList();
            }
        }

        public List<CategoryRecord> GetCategoriesRecordsByLanguage(int languageID, int? pageNo = 1, int? recordSize = 0)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categoryRecords = context.CategoryRecords
                                         .Where(x => x.LanguageID == languageID && !x.IsDeleted)
                                         .OrderBy(x => x.ID)
                                         .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    categoryRecords = categoryRecords.Skip(skip)
                                                     .Take(recordSize.Value);
                }

                return categoryRecords.ToList();
            }
        }

        public List<Category> GetAllTopLevelCategories(int? pageNo = 1, int? recordSize = 0)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categories = context.Categories
                                    .Where(x => !x.ParentCategoryID.HasValue && !x.IsDeleted)
                                    .OrderBy(x => x.ID).Include(x => x.CategoryRecords).Include(p => p.Products).ThenInclude(pc => pc.ProductRecords).Include(asd=> asd.Products).ThenInclude(ps=>ps.ProductPictures)
                                    .AsQueryable();

                if (recordSize.HasValue && recordSize.Value > 0)
                {
                    pageNo = pageNo ?? 1;
                    var skip = (pageNo.Value - 1) * recordSize.Value;

                    categories = categories.Skip(skip)
                                           .Take(recordSize.Value);
                }

                return categories.ToList();
            }
        }

        public Category GetCategoryByID(int ID)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var category = context.Categories.Find(ID);
                
                return category != null && !category.IsDeleted ? category : null;
            }
        }

        public CategoryRecord GetCategoryRecordByID(int ID)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var categoryRecord = context.CategoryRecords.Find(ID);

                return categoryRecord != null && !categoryRecord.IsDeleted ? categoryRecord : null;
            }
        }

        public Category GetCategoryByName(string sanitizedCategoryName)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var category = context.Categories.FirstOrDefault(x => x.SanitizedName.Equals(sanitizedCategoryName));

                return category != null && !category.IsDeleted ? category : null;
            }
        }

        public bool SaveCategory(Category category)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                context.Categories.Add(category);

                return context.SaveChanges() > 0;
            }
        }

        public bool SaveCategoryRecord(CategoryRecord categoryRecord)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                context.CategoryRecords.Add(categoryRecord);

                return context.SaveChanges() > 0;
            }
        }

        public bool UpdateCategory(Category category)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var existingCategory = context.Categories.Find(category.ID);

                context.Entry(existingCategory).CurrentValues.SetValues(category);

                return context.SaveChanges() > 0;
            }
        }
        
        public bool UpdateCategoryRecord(CategoryRecord categoryRecord)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var existingRecord = context.CategoryRecords.Find(categoryRecord.ID);

                context.Entry(existingRecord).CurrentValues.SetValues(categoryRecord);

                return context.SaveChanges() > 0;
            }
        }

        public bool DeleteCategory(int ID)
        {
            var options = DataContextHelper.GetNewContext();

            using (var context = new projectWEBContext(options))
            {
                var category = context.Categories.Find(ID);

                category.IsDeleted = true;

                context.Entry(category).State = EntityState.Modified;

                return context.SaveChanges() > 0;
            }
        }

        public List<Category> SearchCategories(int? parentCategoryID, string searchTerm, int? pageNo, int recordSize, out int count)
        {
            //var options = new DbContextOptionsBuilder<projectWEBContext>()
            //                 .UseSqlServer(_connectionString)
            //                 .Options;
            var options = DataContextHelper.GetNewContext();


            using (var context = new projectWEBContext(options))
            {
                var categories = context.Categories.Where(x => !x.IsDeleted).AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    categories = context.CategoryRecords
                                        .Where(x => !x.Category.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                        .Select(x => x.Category).Include(x => x.CategoryRecords).Include(p => p.Products).ThenInclude(pc => pc.ProductRecords).Include(asd => asd.Products).ThenInclude(ps => ps.ProductPictures)
                                        .AsQueryable();
                }

                if (parentCategoryID.HasValue && parentCategoryID.Value > 0)
                {
                    categories = categories.Where(x => x.ParentCategoryID == parentCategoryID.Value);
                }

                count = categories.Count();

                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * recordSize;

                return categories.OrderBy(x => x.ID).Skip(skipCount).Take(recordSize).ToList();
            }
        }
    }
}

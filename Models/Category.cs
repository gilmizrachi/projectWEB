using projectWEB.Models;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Data
{
    public class Category : BaseEntity
    {
        [Required]
        public string name { get; set; }

        public int? ParentCategoryID { get; set; }
        public virtual Category ParentCategory { get; set; }
        public bool isFeatured { get; set; }
        public string SanitizedName { get; set; }

        public int DisplaySeqNo { get; set; }

        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }

        public virtual List<Item> Products { get; set; }
        public virtual List<CategoryRecord> CategoryRecords { get; set; }

    }

    public class CategoryRecord : BaseEntity
    {
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int LanguageID { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}

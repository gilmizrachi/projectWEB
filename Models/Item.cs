using Microsoft.AspNetCore.Identity;
using projectWEB.Models;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace projectWEB.Data
{
    public class Item : BaseEntity
    {
        public string ProductName { get; set; }
        public int price { get; set; }
        public string ItemDevision { get; set; }
        public string Description { get; set; }
        public int amount { get; set; }

        //public Blob Image { get; set; }



        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public decimal? Discount { get; set; }
        public decimal? Cost { get; set; }
        public bool isFeatured { get; set; }
        public int ThumbnailPictureID { get; set; }

        public string SKU { get; set; }
        public string Tags { get; set; }
        public string Barcode { get; set; }
        public string Supplier { get; set; }

        public virtual List<ProductPicture> ProductPictures { get; set; }

        public virtual List<ProductRecord> ProductRecords { get; set; }
    }

    public class ProductRecord : BaseEntity
    {
        public int ProductID { get; set; }
        public virtual Item Item { get; set; }

        public int LanguageID { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }
}

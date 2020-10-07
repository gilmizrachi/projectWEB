﻿using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class ProductSpecification : BaseEntity
    {
        public int ProductRecordID { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }

    public enum ProductSpecifications
    {
        Brand = 1,
        Model = 2,
        Material = 3,
        Color = 4,
        Weight = 5,
        Shape = 6,
        Size = 7,
        Dimensions = 8,
        Height = 9,
        Length = 10,
    }
}

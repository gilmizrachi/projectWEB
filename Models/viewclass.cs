using System;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using projectWEB.Data;

namespace projectWEB.Models
{
    public class viewclass
    {
        public Reviews Reviews { get; set; }
        public RegisteredUsers registeredUsers { get; set; }
    }
    public class ChartData
    {
        /*  public ChartData(string label, double y)
          {
              this.Label = label;
              this.Y = y;
          }
          [DataMember(Name = "label")]
          public string Label { get; set; } = "";

          [DataMember(Name = "y")]
          public Nullable<double> Y { get; set; } = null; */
        public ChartData(string label, double y)
        {
            this.sale_time = label;
            this.value = y;
        }
        [DataMember(Name = "sale_time")]
        public string sale_time { get; set; } = "";

        [DataMember(Name = "value")]
        public Nullable<double> value { get; set; } = null;
    }
    public class BufferedSingleFileUploadDbModel 
    {

        public itemview FileUpload { get; set; }

}

    public class itemview
    {
        public itemview() { }
            public itemview(Item cpy)
        {
            id = cpy.id;
            ItemName = cpy.ItemName;
            price = cpy.price;
            ItemDevision = cpy.ItemDevision;
            Description = cpy.Description;
            amount = cpy.amount;
            
        }
        public int id { get; set; }

        public string ItemName { get; set; }
        public int price { get; set; }

        public string ItemDevision { get; set; }

        public string Description { get; set; }

        public int amount { get; set; }

        public IFormFile FormFile { get; set; }
    }

}

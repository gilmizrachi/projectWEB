using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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

 }

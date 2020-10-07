using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class LanguageResource : BaseEntity
    {
        public string Key { get; set; }
        public int LanguageID { get; set; }
        public string Value { get; set; }
    }
}

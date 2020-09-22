using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public ICollection<Item> Cart { get; set; }
        public DateTime TranscationDate { get; set; }
        public RegisteredUsers Customer { get; set; }
        public float SumPrice { get; set; }
        public int MyProperty { get; set; }
    }
}

using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public enum Status
    {
        Pending,
        Approved,
        Paid,
        Completed
    }
    public class Transaction
    {
        public int Id { get; set; }
        public ICollection<Item> Cart { get; set; }
        public DateTime TranscationDate { get; set; }
        public RegisteredUsers Customer { get; set; }
        public float SumPrice { get; set; }
        public Status Status { get; set; }
        public int CustomerId { get; set; }
        
    }
}

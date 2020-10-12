using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public void AddCart(Item CartItem)
        {
            if (this.Cart == null)
            {
                this.Cart = new List<Item>();
                Cart.Add(CartItem);
            }
            else
            {
                Cart.Add(CartItem);
            }
            this.SumPrice += CartItem.price;
        }
        public List<Item> GetCart()
        {
            return this.Cart;
        }
        public int Id { get; set; }
        public  List<Item> Cart { get; set; }
        [Display(Name ="Commit Date")]
        public DateTime TranscationDate { get; set; }
        public RegisteredUsers Customer { get; set; }
        [Display(Name = "Total Value")]
        public float SumPrice { get; set; }
        public Status Status { get; set; }
        public int CustomerId { get; set; }
        
    }
}

using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Order : BaseEntity
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        //public int id { get; set; }
        public int item_id { get; set; }
        public int item_quantity { get; set; }
        public DateTime date { get; set; }
        public int user_id { get; set; }




        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerZipCode { get; set; }

        public string OrderCode { get; set; }
        public int PaymentMethod { get; set; }
        public decimal TotalAmmount { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryCharges { get; set; }
        public decimal FinalAmmount { get; set; }
        public DateTime PlacedOn { get; set; }

        public string TransactionID { get; set; }

        public bool IsGuestOrder { get; set; }

        public int? PromoID { get; set; }
        public virtual Promo Promo { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual List<OrderHistory> OrderHistory { get; set; }
    }
    public enum PaymentMethods
    {
        CreditCard = 1,
        PayPal = 2,
        CashOnDelivery = 3
    }
    public enum OrderStatus
    {
        Placed = 1,
        Processing = 2,
        Delivered = 3,
        Failed = 4,
        Cancelled = 5,
        OnHold = 6,
        WaitingForPayment = 7,
        Refunded = 8
    }
}

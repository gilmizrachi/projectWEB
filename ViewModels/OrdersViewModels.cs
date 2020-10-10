using projectWEB.Models;
using projectWEB.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace projectWEB.ViewModels
{
    public class TrackOrderViewModel : PageViewModel
    {
        public int? OrderID { get; set; }
        public string CustomerEmail { get; set; }
        public Order Order { get; set; }
    }

    public class PrintInvoiceViewModel : PageViewModel
    {
        public Order Order { get; set; }
        public int? OrderID { get; set; }
    }
    public class AuthorizeNetCreditCardModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Card Holder Name can only be 100 characters at max.")]
        [Display(Name = "Card Holder Name")]
        public string CCName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [CreditCard(ErrorMessage = "Card Number is not valid credit card number.")]
        public string CCCardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Month")]
        [Range(1, 12)]
        public short CCExpiryMonth { get; set; }

        [Required]
        [Display(Name = "Expiry Year")]
        public int CCExpiryYear { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "CVC must be 3 characters.")]
        [Display(Name = "CVC")]
        public string CCCVC { get; set; }
    }
    public class PlaceOrderCrediCardViewModel : AuthorizeNetCreditCardModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        public int PromoID { get; set; }
        public decimal Discount { get; set; }

        public bool CreateAccount { get; set; }

        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }
    public class PlaceOrderCashOnDeliveryViewModel
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

        public int PromoID { get; set; }
        public decimal Discount { get; set; }

        public bool CreateAccount { get; set; }

        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }

    public class PlaceOrderPayPalViewModel : PlaceOrderCashOnDeliveryViewModel
    {
        public string TransactionID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
    }

    public class UserOrdersViewModel
    {
        public int? OrderID { get; set; }
        public int? OrderStatus { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public RegisteredUsers User { get; set; }
        public List<Order> UserOrders { get; set; }
        public Pager Pager { get; set; }
    }
}
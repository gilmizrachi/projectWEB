using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public enum MemberType
    {
        BasicUser,
        SalesPerson,
        Supervisor,
        Admin
    }
    public class RegisteredUsers
    {
        public int id { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [MinLengthAttribute(3)]
        [MaxLength(15)]
        public string UserName { get; set; }

        
        [MinLengthAttribute(8)]
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [MinLengthAttribute(8)]
        [MaxLength(30)]
       
        public string Email { get; set; }

        private string _Password { get; set; }

        [DataType(DataType.CreditCard)]
        private string CreditCardNo { get; set; }

        [RegularExpression(@"^[0-9]*$")]
        [Display(Name = "Credit Card Info")]
        public string CreditCard
        {
            get
            {
                if (string.IsNullOrEmpty(CreditCardNo)) return "✘";
                else return "✅";
            }
            set
            {
                if (value.Length > 1)
                { CreditCardNo = value; }
            }

        }

        public  MemberType MemberType { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}

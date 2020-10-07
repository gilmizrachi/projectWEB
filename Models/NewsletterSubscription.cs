using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class NewsletterSubscription : BaseEntity
    {
        public string EmailAddress { get; set; }
        public bool IsVerified { get; set; }
        public string UserID { get; set; }
    }
}

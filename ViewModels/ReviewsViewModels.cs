using projectWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projectWEB.ViewModels
{
    public class ReviewsViewModel
    {
        public string Text { get; set; }

        public int Rating { get; set; }

        public int EntityID { get; set; }

        public int RecordID { get; set; }
    }

    public class EntityReviewsViewModel : PageViewModel
    {
        public List<Reviews> Reviews { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }
    }
    public class REviewsListingViewModel : PageViewModel
    {
        public string SearchTerm { get; set; }
        public RegisteredUsers User { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<Product> ReviewedProducts { get; set; }
        public Pager Pager { get; set; }
    }
}
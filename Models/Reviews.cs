using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public RegisteredUsers registeredUsers { get; set; }
        public string CommentTitle { get; set; }

        public string CommentBody { get; set; }

        public int Rate { get; set; }
        public DateTime PublishTime { get; set; }
    }
}

using projectWEB.Data;
using projectWEB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWEB.Models
{
    public class Reviews : BaseEntity
    {
        public Item item { get; set; }

        public string CustomerName { get; set; }

        public string Comment { get; set; }
        public DateTime PublishTime { get; set; }
        public string UserID { get; set; }
        public virtual RegisteredUsers User { get; set; }

        public int Rating { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }

        public int LanguageID { get; set; }
    }
}

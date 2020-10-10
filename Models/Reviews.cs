using projectWEB.Data;
using projectWEB.Services;
using System;
using System.Collections.Generic;


namespace projectWEB.Models
{
    public class Reviews : BaseEntity
    {
        public DateTime TimeStamp { get; set; } 
        public string UserID { get; set; }
        public virtual RegisteredUsers User { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }
        public int LanguageID { get; set; }
    }
}

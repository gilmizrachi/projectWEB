using Microsoft.EntityFrameworkCore.Infrastructure;
using projectWEB.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectWEB.Models
{
   
    public class AlsoTry
    {
        private const char DataSeparator = ',';
        public List<string> Getval(string str)
        {
            string[] temparr = str.Split(DataSeparator);
            List<string> vals = new List<string>();
            foreach (var t in temparr)
            {
                vals.Add(t);
            }
            return vals;
        }
        public int Similarity(List<string> vals)
        {
            int res = 0;
            foreach(var i in vals)
            {
                if (this.S_Phrase.Contains(i))
                    res += 1;
            }
            return res;
        }
        public List<int> Budget()
        {
            var templimit = this.PriceLimits;
            if (templimit == null)
            {
                return null;
            }
            var result = new List<int>();
            string[] temparr = templimit.Split(DataSeparator);
            foreach(var t in temparr)
            {
                int price;
                if (!int.TryParse(t, out price))
                {
                    throw new ArgumentException("string price didnt converted to int");
                }
                result.Add(price);
            }
            return result;
        }
        public void AddSearchByPrice(int maxprice)
        {
            
            if (PriceLimits==null)
            {
                var newlimit = new StringBuilder();
                newlimit.Append(maxprice.ToString());
                newlimit.Append(DataSeparator);
                this.PriceLimits = newlimit.ToString();
            }
            else
            {
                var templimit = new StringBuilder();
                templimit.Append(this.PriceLimits);
                templimit.Append(maxprice.ToString());
                templimit.Append(DataSeparator);
                this.PriceLimits = templimit.ToString();
            }
        }

        public void AddSearchedPhrase(string phrase)
        {

            if (this.S_Phrase == null)
            {
                var newphrase = new StringBuilder();
                newphrase.Append(phrase);
                newphrase.Append(DataSeparator);
                this.S_Phrase = newphrase.ToString();
            }
            else
            {
                var tempphrase = new StringBuilder();
                tempphrase.Append(this.S_Phrase);
                tempphrase.Append(phrase);
                tempphrase.Append(DataSeparator);
                this.S_Phrase = tempphrase.ToString();
            }
        }
        public int Id { get; set; }
        public RegisteredUsers registeredUsers { get; set; }

        [Display(Name = "Searched Phrases")]
        public String S_Phrase { get; set; } //list of user searches converted to string

        [Display(Name = "Visited Items")]
        public List<Item> V_Items { get; set; } = new List<Item>();//list of the items viewed
        public int V_ItemNo { get; set; } = 0; //Number of items viewed by the user before purchase
        public Transaction Transaction { get; set; } // Final transaction that seals this record
        public String PriceLimits { get; set; } // user price limitations on search - estimate for customer budget.


        public bool IsActive { get; set; } = true; // once purchase is commited, this record become archived for future indexing
        

    }
}

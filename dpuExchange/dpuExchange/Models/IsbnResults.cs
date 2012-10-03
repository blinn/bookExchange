using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.Entity;
using System.Collections.ObjectModel;
namespace dpuExchange.Models
{
    public class IsbnResults
    {
        public virtual string Isbn { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
    }

    public class IsbnResultList
    {
        public IsbnResults result1 = new IsbnResults();
        public IsbnResults result2 = new IsbnResults();
        public IsbnResults result3 = new IsbnResults();
        public IsbnResults result4 = new IsbnResults();
        public int numRecords;
    }
}
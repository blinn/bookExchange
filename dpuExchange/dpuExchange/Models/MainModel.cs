using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dpuExchange.Models
{
    public class MainModel
    {
        public Books BooksModel { get; set; }
        public IsbnResults IsbnResultsModel { get; set; }
        public Searches SearchModel { get; set; }
        public IsbnResultList IsbnCollection { get; set; }
        IsbnResultList test = new IsbnResultList();
    }
}
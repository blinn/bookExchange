using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dpuExchange.Models
{
    public class DetailsModel
    {
        public IEnumerable<dpuExchange.Models.Books> AllBooks { get; set; }
        public Books Book { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dpuExchange.Models
{
    public class Searches
    {
        public virtual string SearchByTitle { get; set; }
        public virtual string SearchByIsbn { get; set; }
        public virtual string SearchForPrices { get; set; }

    }
}
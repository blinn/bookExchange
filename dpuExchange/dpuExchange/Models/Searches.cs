using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Model used by MainModel.cs
namespace dpuExchange.Models
{
    public class Searches
    {
        public virtual string SearchByTitle { get; set; }
        public virtual string SearchByIsbn { get; set; }
    }
}
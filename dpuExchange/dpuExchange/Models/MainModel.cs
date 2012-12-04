using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Model used for ISBN title lookup on Books/Create
namespace dpuExchange.Models
{
    public class MainModel
    {
        public Books BooksModel { get; set; }
        public Searches SearchModel { get; set; }
    }
}
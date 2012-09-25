using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace dpuExchange.Models
{
    public class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PostDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Condition { get; set; }
        public string Comment { get; set; }
    }

    public class BookDBContext : DbContext
    {
        public DbSet<Books> Book { get; set; }
    }
}
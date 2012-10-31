using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace dpuExchange.Models
{
    public class Books
    {   [Key]
        public virtual Guid BookID { get; set; }
        public virtual string UserID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual double Price { get; set; }
        public virtual string Condition { get; set; }
        public virtual string Comments { get; set; }
        public virtual DateTime PostDate { get; set; }
        public virtual string Isbn { get; set; }
        public virtual string ClassCode { get; set; }
    }

    public class BooksDBContext : DbContext
    {
        public DbSet<Books> BookItems { get; set; }
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace dpuExchange.Models
{
    public class EmailModel
    {
        public virtual string Sender { get; set; }
        public virtual string Seller { get; set; }
        [Required]
        public virtual string Message { get; set; }
        public virtual string BookTitle { get; set; }
    }
}
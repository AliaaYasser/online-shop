using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{
    public class category
    {
        [Key]
       public int categoryid { set; get; }
        public String name { set; get; }
        public ICollection<product> products { get; set; }
    }
}
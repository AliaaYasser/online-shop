using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{
    public class product
    {
        [Key]
        public int id { set; get; }
        public String name { set; get; }
        public float price { set; get; }
        public String Description { set; get; }
        public string image { set; get; }
        [ForeignKey("category")]
        public int categoryid { set; get; }
        public category category { set; get; }
        
        public product()
        {
            this.userapps = new HashSet<Userapp>();
        }
        public virtual ICollection<Userapp> userapps { get; set; }
    }
}
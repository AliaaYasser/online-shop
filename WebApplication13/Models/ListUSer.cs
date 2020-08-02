using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication13.Models
{
    public class ListUSer
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<Userapp> userapps { get; set; }
    }
}
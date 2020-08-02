using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication13.Models
{
   
    public class Error
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Userapp userapps{ get; set; }
        
    }
}
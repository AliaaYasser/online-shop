using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;
using WebApplication13.Models;

namespace WebApplication13.Models
{
    public class Userapp
    {
        [Key]
        
        public int userId
        {
            get; set;
        }

        [Required]
        [Display(Name = "name")]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        [MinLength(3, ErrorMessage = "Your phine number less than 3 numbers.")]
        [MaxLength(20, ErrorMessage = "Your phine number is too long, the maximum is 20 character.")]
        [Phone]
        [Display(Name = "phone")]
        public string phone { get; set; }

        [Required]
        [MaxLength(256, ErrorMessage = "Your  address is too long, the maximum is 256 character.")]
        [Display(Name = "country")]
        public string country
        { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "image")]
        public string image { get; set; }

        public string uR { get; set; }
        public Userapp()
        {
            this.Products = new HashSet<product>();
        }
        public virtual ICollection<product> Products { get; set; }


    }
}
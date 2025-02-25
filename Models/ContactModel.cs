using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace Job_Offers_WebSite3.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
      
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
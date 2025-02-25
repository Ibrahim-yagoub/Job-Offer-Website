using System;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace Job_Offers_WebSite3.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime      ApplyDate   { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}
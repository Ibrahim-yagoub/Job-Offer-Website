using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Job_Offers_WebSite3.Models
{
    public class Job
    {
        [Required]
        public int JobId { get; set; }
        [Required]
        [DisplayName("إسم الوظيفة")]
        public string JobTitle { get; set; }
        [Required]
        [DisplayName("وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }
        [Required]
        [DisplayName("صورة الوظيفة")]
        public string JobImage { get; set; }
        [Required]
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }
        public string  UserID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
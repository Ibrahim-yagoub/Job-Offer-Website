using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Offers_WebSite3.Models
{
    public class JobsViewModels
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}
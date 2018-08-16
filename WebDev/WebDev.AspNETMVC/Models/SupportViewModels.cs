using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDev.AspNETMVC.Models
{
    public class SupportViewModels
    {

    }
    public class IssueReport
    {
        public HttpPostedFileBase Attachment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }

}
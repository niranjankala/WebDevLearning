using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDevAspNet.WebApiCors.Models
{
    public class Project
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }

        public string Unit { get; set; }
    }
}
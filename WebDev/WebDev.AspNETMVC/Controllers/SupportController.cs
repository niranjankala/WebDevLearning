using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDev.AspNETMVC.Models;

namespace WebDev.AspNETMVC.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportIssue()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReportIssue([Bind(Include = "Name,Email,Subject,Message")]IssueReport issueMessage, HttpPostedFileBase upload)
        {
            return View(issueMessage);
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDev.AspNETMVC.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadInstallerX64(string returnUrl = null)
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Register", "Account", new { ReturnUrl = Url.Action("DownloadInstallerX64") });

            string setupPathX64 = @"/Downloads/Installer/x64/";

            //SendPostDownloadEmail(user);
            //LogDownloadEvent(user.Id, GetSimergyTrialDownloadName(setupPathX64));

            if (returnUrl == null)
                returnUrl = Url.Action("Index", "Home", new { }); //new { area = "Simergy.Common" });
            //DownloadLatestTrial(setupPathX64, returnUrl);

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/" + setupPathX64));
            FileInfo[] files = dir.GetFiles();

            if (files.Length != 0)
            {
                FileInfo firstFile = files[0];
                setupPathX64 += firstFile.Name;
                ViewData["filePath"] = setupPathX64;
                ViewData["returnUrl"] = returnUrl;
            }

            //Response.Redirect(returnUrl);
            return View();

        }

        private void DownloadLatestTrial(string folderPath, string returnUrl)
        {
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/" + folderPath));
            FileInfo[] files = dir.GetFiles();

            if (files.Length == 0)
                return;

            FileInfo firstFile = files[0];

            Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + firstFile.Name;
            if (!string.IsNullOrWhiteSpace(returnUrl))
                //Response.AddHeader("Location", returnUrl);
                Response.AddHeader("Refresh", $"1; url={returnUrl}");
            Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(Server.MapPath("~/" + folderPath + firstFile.Name));
            Response.WriteFile(Dfile.FullName);
            Response.Flush();
            if (!string.IsNullOrWhiteSpace(returnUrl))
                Response.Redirect(returnUrl, false);
            //Response.End();

        }
    }
}
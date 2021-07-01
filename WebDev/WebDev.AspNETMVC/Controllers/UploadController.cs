using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDev.AspNETMVC.Models;
using WebDev.Common;
using WebDev.Common.Enums;

namespace WebDev.AspNETMVC.Controllers
{
    public class UploadController : Controller
    {
        private FileServices fileServices;
        public UploadController()
        {
            this.fileServices = new FileServices();
        }

        // GET: Upload
        public ActionResult Index()
        {
            return View(new UploadViewModel() {  });
        }

        public string Upload(HttpPostedFileBase fileData)
        {
            try
            {
                
                if (!Directory.Exists(Server.MapPath(DirectoryPaths.TeamAvatars)))
                {
                    Directory.CreateDirectory(Server.MapPath(DirectoryPaths.TeamAvatars));
                }
                var fileName = this.fileServices.UploadAvatar(fileData, UploadAvatarType.TeamAvatar);
                return Url.Content(DirectoryPaths.TeamAvatars + "\\" + fileName);
            }
            catch (Exception ex)
            {
               // if (_log.IsErrorEnabled) _log.Error(ex.Message, ex);
                throw ex;
            }
        }

        public ActionResult Uploadifive()
        {
            return View(new UploadViewModel() { });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using WebDev.Common.Enums;

namespace WebDev.Common
{
    public class FileServices
    {

        public string UploadAvatar(HttpPostedFileBase avatar, UploadAvatarType uploadAvatarType)
        {
            string random_name = Randoms.CleanGUID() + ".png";
            string filePath = string.Empty;

            if (uploadAvatarType == UploadAvatarType.UserAvatar)
            {
                while (File.Exists(Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.UserAvatars), random_name)))
                    random_name = Randoms.CleanGUID() + ".png";

                filePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.UserAvatars), random_name);
            }
            else if (uploadAvatarType == UploadAvatarType.GroupAvatar)
            {
                while (File.Exists(Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.GroupAvatars), random_name)))
                    random_name = Randoms.CleanGUID() + ".png";

                filePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.GroupAvatars), random_name);
            }

            else if (uploadAvatarType == UploadAvatarType.TeamAvatar)
            {
                while (File.Exists(Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.TeamAvatars), random_name)))
                    random_name = Randoms.CleanGUID() + ".png";

                filePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.TeamAvatars), random_name);
            }

            

            int maxHeight = Convert.ToInt32(SiteConfig.AvatarHeight);
            int maxWidth =  Convert.ToInt32(SiteConfig.AvatarWidth);

            using (Image ravatar = Image.FromStream(avatar.InputStream, true, true))
            {
                if (ravatar.Width > maxWidth || ravatar.Height > maxHeight)
                {
                    double ratio = (double)ravatar.Width / ravatar.Height;
                    double newHeight;
                    double newWidth;

                    if (ravatar.Width > ravatar.Height)
                    {
                        ratio = 1 / ratio;
                        newWidth = maxWidth;
                        newHeight = maxHeight * ratio;
                    }
                    else
                    {
                        newWidth = maxWidth * ratio;
                        newHeight = maxHeight;
                    }

                    using (var resizedAvatar = ravatar.GetThumbnailImage((int)newWidth, (int)newHeight, null, IntPtr.Zero))
                    {
                        resizedAvatar.Save(filePath, ImageFormat.Png);
                    };
                }
                else
                    ravatar.Save(filePath, ImageFormat.Png);
            }
            return random_name;
        }

        public string UploadAvatar(byte[] avatar, UploadAvatarType uploadAvatarType)
        {
            string random_name = Randoms.CleanGUID() + ".png";
            string filePath = string.Empty;

            if (uploadAvatarType == UploadAvatarType.UserAvatar)
            {
                while (File.Exists(Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.UserAvatars), random_name)))
                    random_name = Randoms.CleanGUID() + ".png";

                filePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.UserAvatars), random_name);
            }
            else if (uploadAvatarType == UploadAvatarType.GroupAvatar)
            {
                while (File.Exists(Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.GroupAvatars), random_name)))
                    random_name = Randoms.CleanGUID() + ".png";

                filePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.GroupAvatars), random_name);
            }

            int maxHeight = Convert.ToInt32(SiteConfig.AvatarHeight);
            int maxWidth = Convert.ToInt32(SiteConfig.AvatarWidth);

            MemoryStream ms = new MemoryStream(avatar,0,avatar.Length);
            using (Image ravatar = Image.FromStream(ms))
            {
                if (ravatar.Width > maxWidth || ravatar.Height > maxHeight)
                {
                    double ratio = (double)ravatar.Width / ravatar.Height;
                    double newHeight;
                    double newWidth;

                    if (ravatar.Width > ravatar.Height)
                    {
                        ratio = 1 / ratio;
                        newWidth = maxWidth;
                        newHeight = maxHeight * ratio;
                    }
                    else
                    {
                        newWidth = maxWidth * ratio;
                        newHeight = maxHeight;
                    }

                    using (var resizedAvatar = ravatar.GetThumbnailImage((int)newWidth, (int)newHeight, null, IntPtr.Zero))
                    {
                        resizedAvatar.Save(filePath, ImageFormat.Png);
                    };
                }
                else
                    ravatar.Save(filePath, ImageFormat.Png);
            }
            return random_name;
        }

        public string UploadFile(HttpPostedFileBase file, Guid folderId)
        {
            string fileName = Path.GetFileName(file.FileName);
            string folder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString());
            string filePath = Path.Combine(folder, fileName);           

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (File.Exists(filePath))
                fileName = Randoms.CleanGUID() + fileName;
            string path = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString(), fileName);
            file.SaveAs(path);

            return fileName;
        }

        public string UploadProjectStepFile(HttpPostedFileBase file, Guid parentFolderId, Guid childFolderId)
        {
            string fileName = Path.GetFileName(file.FileName);
            string folder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), parentFolderId.ToString(), childFolderId.ToString());
            string filePath = Path.Combine(folder, fileName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (File.Exists(filePath))
            fileName = Randoms.CleanGUID() + fileName;
            string path = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), parentFolderId.ToString(), childFolderId.ToString(), fileName);
            file.SaveAs(path);

            return fileName;
        }

        public string DeleteUploadedFile(string documentName, Guid folderId , string deletedPath)
        {
            try
            {
                string fileName = documentName;
                string folder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString());
                string filePath = Path.Combine(folder, fileName);
                string deletedFolder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString(), "DeletedFiles");
                string deletdeFolderdate = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString(), "DeletedFiles", deletedPath);
                if (File.Exists(filePath))
                {
                    if (!Directory.Exists(deletdeFolderdate))
                        Directory.CreateDirectory(deletdeFolderdate);
                    string path = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString(), "DeletedFiles", deletedPath, fileName);
                    File.Move(filePath, path);
                        
                }
                return deletdeFolderdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public string DeleteUploadedProjectStepFile(string documentName, Guid parentFolderId, Guid childFolderId, string deletedPath)
        {
            try
            {
                string fileName = documentName;
                string folder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), parentFolderId.ToString(), childFolderId.ToString());
                string filePath = Path.Combine(folder, fileName);
                string deletedFolder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), parentFolderId.ToString(), childFolderId.ToString(), "DeletedFiles");
                string deletdeFolderdate = Path.Combine(deletedFolder, deletedPath);
                if (File.Exists(filePath))
                {
                    if (!Directory.Exists(deletdeFolderdate))
                        Directory.CreateDirectory(deletdeFolderdate);
                    string path = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), parentFolderId.ToString(), childFolderId.ToString(), "DeletedFiles", deletedPath, fileName);
                    File.Move(filePath, path);

                }
                return deletdeFolderdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RevertDeletedFile(string deletedDocumentName, Guid folderId)
        {
            try
            {
                string folder = deletedDocumentName;

                if (Directory.Exists(folder))
                {
                    string path = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString());
                    string[] files = Directory.GetFiles(folder);
                    foreach (string file in files)
                    {
                        string name = Path.GetFileName(file);
                        string dest = Path.Combine(path, name);
                        File.Move(file, dest);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletedNewUploadedFile(HttpPostedFileBase file, Guid folderId)
        {
            try
            {
                string fileName = Path.GetFileName(file.FileName); ;
                string folder = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.Attachments), folderId.ToString());
                string filePath = Path.Combine(folder, fileName);
               
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        /// <summary>
        /// No Preview file copy 
        /// </summary>
        /// <param name="targetDir"></param>
        /// <param name="fileName"></param>
        private void addNoPreviewImage(string targetDir, string fileName)
        {
            string sourcefile = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.GroupNavLink), "nopreview.png");
            File.Copy(sourcefile, Path.Combine(targetDir, fileName + Path.GetExtension(sourcefile)), true);
        }
      

        /*************************************************************************************************************/

        public void MoveWarrantyFiles(string sourcePath, string destinationPath)
        {          
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            try
            {
                //Move Warranty Claim Photo
                targetFolder = destinationPath;
                if (Directory.Exists(sourcePath))
                {
                    string[] files = Directory.GetFiles(sourcePath);
                    if (!Directory.Exists(targetFolder))
                        Directory.CreateDirectory(targetFolder);
                    if (files.Length > 0)
                    {
                        foreach (string file in files)
                        {
                            fileName = Path.GetFileName(file);
                            destinationPath = Path.Combine(targetFolder, fileName);
                            File.Copy(file, destinationPath, true);
                        }
                    }
                    //Delete All Files on Source Path
                    foreach (string file in files)
                    {
                        fileName = Path.GetFileName(file);
                        destinationPath = Path.Combine(sourcePath, fileName);
                        File.Delete(destinationPath);
                    }
                    if (Directory.GetFiles(sourcePath).Length == 0)
                        Directory.Delete(sourcePath);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /***************************************************************************************************************/

        public void DeleteWarrantyFiles(string folderId, string imageType,bool isEdit,string fileToBeDeleted="")
        {
            string sourcePath = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            try
            {
                if (isEdit)
                {
                    sourcePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.PermWarranty), imageType, folderId);
                    if (Directory.Exists(sourcePath)) 
                    {
                        filePath = Path.Combine(sourcePath, fileToBeDeleted);
                        File.Delete(filePath);
                    }
                }
                else 
                { 
                sourcePath = Path.Combine(HostingEnvironment.MapPath(DirectoryPaths.PermWarranty), imageType, folderId);

                    if (Directory.Exists(sourcePath))
                    {
                        string[] files = Directory.GetFiles(sourcePath);

                        //Delete All Files on Source Path
                        foreach (string file in files)
                        {
                            fileName = Path.GetFileName(file);
                            filePath = Path.Combine(sourcePath, fileName);
                            File.Delete(filePath);
                        }

                        if (Directory.GetFiles(sourcePath).Length == 0)
                            Directory.Delete(sourcePath);
                    }
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<string> GetDuplicatefiles(string[] fileList)
        {
            List<string> FileLists = new List<string>();
            foreach (string file in fileList)
            {
                FileLists.Add(Path.GetFileNameWithoutExtension(file));
            }
            var duplicateFiles = FileLists.GroupBy(item => item).SelectMany(grp => grp.Skip(1));
            FileLists = new List<string>();
            foreach (var file in duplicateFiles)
            {
                FileLists.Add(file+".png");
            }
            return FileLists;
        }
    }
}

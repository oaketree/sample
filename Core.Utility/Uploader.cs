using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Utility
{
    public class Uploader
    {
        private string file;
        private List<string> check;
        private Stream uploadStream;
        private FileStream fs;
        private string fileName;
        public Uploader(string file, string[] check)
        {
            this.file = file;
            this.check = check.ToList();
        }

        public Uploader(string[] check)
        {
            this.check = check.ToList();
        }

        public bool checkUpload()
        {
            HttpPostedFile postFile = null;
            if (!string.IsNullOrEmpty(file))
                postFile = HttpContext.Current.Request.Files[file];
            else
                postFile = HttpContext.Current.Request.Files[0];
            if (postFile.FileName == string.Empty)
                return false;
            string fileExt = Path.GetExtension(postFile.FileName).ToLower();
            if (check != null)
            {
                if (!check.Contains(fileExt))
                    return false;
            }
            fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss_fff") + fileExt;
            uploadStream = postFile.InputStream;
            return true;
        }

        public bool checkUpload(int length)
        {
            HttpPostedFile postFile = null;
            if (!string.IsNullOrEmpty(file))
                postFile = HttpContext.Current.Request.Files[file];
            else
                postFile = HttpContext.Current.Request.Files[0];
            if (postFile.FileName == string.Empty)
                return false;
            if (postFile.ContentLength > length)
                return false;
            string fileExt = Path.GetExtension(postFile.FileName).ToLower();
            if (check != null)
            {
                if (!check.Contains(fileExt))
                    return false;
            }
            fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss_fff") + fileExt;
            uploadStream = postFile.InputStream;
            return true;
        }

        public void save(string path, Cut cut, bool isMapPath = true)
        {
            string uploadPath = string.Empty;
            if (isMapPath)
                uploadPath = HttpContext.Current.Server.MapPath(path);
            else
                uploadPath = path;
            new Thumbnail(uploadStream, Path.Combine(uploadPath, fileName), cut);
        }

        public void save(string path, bool isMapPath = true)
        {
            string uploadPath = string.Empty;
            if (isMapPath)
                uploadPath = HttpContext.Current.Server.MapPath(path);
            else
                uploadPath = path;
            int bufferLen = 1024;
            byte[] buffer = new byte[bufferLen];
            int contentLen = 0;
            fs = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create, FileAccess.ReadWrite);
            while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
            {
                fs.Write(buffer, 0, bufferLen);
                fs.Flush();
            }
        }
        public void end()
        {
            if (null != fs)
            {
                fs.Close();
            }
            if (null != uploadStream)
            {
                uploadStream.Close();
            }
        }
        public string FileName
        {
            get
            {
                return fileName;
            }
        }

    }
}

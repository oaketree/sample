using System.IO;
using System.Web;

namespace Core.Utility
{
    public class FileDel
    {
        private string path;
        public FileDel(string path, bool ismapath = true)
        {
            if (ismapath)
                this.path = HttpContext.Current.Server.MapPath(path);
            else
                this.path = path;
            del();
        }
        private void del()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

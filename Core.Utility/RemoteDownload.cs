using System.IO;
using System.Net;

namespace Core.Utility
{
    public class RemoteDownload
    {
        private string path;
        private string httpURL;
        public RemoteDownload(string path, string httpURL)
        {
            this.path = path;
            this.httpURL = httpURL;
        }

        private bool download()
        {
            FileStream fs = null;
            WebClient webClient = null;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read);
                webClient = new WebClient();
                webClient.Credentials = CredentialCache.DefaultCredentials;
                webClient.Headers.Add(HttpRequestHeader.Referer, httpURL);
                byte[] data = webClient.DownloadData(httpURL);
                fs.Write(data, 0, data.Length);
                fs.Flush();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                    fs = null;
                }
                if (null != webClient)
                {
                    webClient.Dispose();
                    webClient = null;
                }
            }

        }
    }
}

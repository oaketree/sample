using System.Web;

namespace Core.Utility
{
    public class Utils
    {
        public static string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}

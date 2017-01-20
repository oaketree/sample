using System.Threading.Tasks;
using System.Web;

namespace Core.Utility
{
    public class Utils
    {
        //HttpContext.Current多线程为null
        public static string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
            //return result;
        }
    }
}

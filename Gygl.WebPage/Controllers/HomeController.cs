using System.Web.Mvc;
using System.Web.Services;

namespace Gygl.WebPage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Top()
        {
            return PartialView();
        }

        public PartialViewResult Bottom()
        {
            return PartialView();
        }

        public PartialViewResult Header()
        {
            return PartialView();
        }
        /// <summary>
        /// session不自动关闭
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void PokePage()
        {

        }
    }
}
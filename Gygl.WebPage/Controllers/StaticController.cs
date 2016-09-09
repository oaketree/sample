using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class StaticController : Controller
    {

        [OutputCache(Duration = 1200)]
        public ActionResult Order()
        {
            return View();
        }

        [OutputCache(Duration = 1200)]
        public ActionResult Introduction()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult Faq()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult Contact()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult Notice()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult Advertisement()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult EssayNotice()
        {
            return View();
        }
        [OutputCache(Duration = 1200)]
        public ActionResult Council()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class StaticController : Controller
    {

        // GET: Static
        public ActionResult Order()
        {
            return View();
        }

        public ActionResult Introduction()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Notice()
        {
            return View();
        }

        public ActionResult Advertisement()
        {
            return View();
        }

        public ActionResult EssayNotice()
        {
            return View();
        }

        public ActionResult Council()
        {
            return View();
        }
    }
}
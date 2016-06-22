using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cms.WebPage.Areas.ComManage.Controllers
{
    public class HomeController : Controller
    {
        // GET: ComManage/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
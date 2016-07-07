using Gygl.BLL.News.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class NewsController : Controller
    {
        [Dependency]
        public INewsService NewsService { get; set; }
        public JsonResult GetNewsList(int count)
        {
            return Json(NewsService.getNewsList(101,count),JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPagedNewsList(int page)
        {
            return Json(NewsService.getPagedNewsList(20, page), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNews(int newsId)
        {
            return Json(NewsService.getNewsById(newsId), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ReadNews()
        {
            return PartialView();
        }


        public PartialViewResult NewsList()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
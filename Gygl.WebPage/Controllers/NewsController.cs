﻿using Gygl.BLL.News.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class NewsController : Controller
    {
        [Dependency]
        public INewsService NewsService { get; set; }

        [OutputCache(Duration = 120,VaryByParam ="count")]
        public async Task<JsonResult> GetNewsList(int count)
        {
            var result = await NewsService.getNewsList(101, count);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 120, VaryByParam = "page")]
        public async Task<JsonResult> GetPagedNewsList(int page)
        {
            var result = await NewsService.getPagedNewsList(20, page);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetNews(int newsId)
        {
            var result = await NewsService.getNewsById(newsId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 120)]
        public PartialViewResult ReadNews()
        {
            return PartialView();
        }

        [OutputCache(Duration = 120)]
        public PartialViewResult NewsList()
        {
            return PartialView();
        }

        [OutputCache(Duration = 120)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
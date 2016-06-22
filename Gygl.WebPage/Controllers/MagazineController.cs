using Gygl.BLL.Magazine.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class MagazineController : Controller
    {
        [Dependency]
        public IGyglCategoryService GyglCategoryService { get; set; }

        [Dependency]
        public IGyglService GyglService { get; set; }

        [Dependency]
        public IImageService ImageService { get; set; }


        [Dependency]
        public IArticleService ArticleService { get; set; }


        public PartialViewResult Catalog(int id)
        {
            return PartialView(GyglCategoryService.getCatalogByID(id));
        }

        public PartialViewResult RightNav()
        {
            return PartialView();
        }


        public ActionResult Periodical(int id)
        {
            return View(GyglService.getPeriodicalById(id));
        }

        public ActionResult GetPages(int aid)
        {
            return Json(ImageService.getArticlePages(aid),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetArticleList(int pid)
        {
            return Json(ArticleService.getArticleList(pid),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFirstPages(int pid)
        {
            return Json(ImageService.getFirstPages(pid), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Search()
        {
            return PartialView();
        }

    }
}
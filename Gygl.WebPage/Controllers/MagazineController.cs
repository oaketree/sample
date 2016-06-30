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

        [Dependency]
        public ICategoryService CategoryService { get; set; }


        public PartialViewResult Catalog(int id)
        {
            return PartialView(GyglCategoryService.getCatalogByID(id));
        }

        public PartialViewResult RightNav()
        {
            return PartialView();
        }


        public PartialViewResult Periodical(int id)
        {
            var ret = GyglService.getPeriodicalById(id);
            if (ret != null)
                return PartialView(GyglService.getPeriodicalById(id));
            else
                return null;
        }

        public PartialViewResult SelectYear(int? year,int? period, int page = 1)
        {
            return PartialView(GyglService.getPeriodicalByYear(page,year, period,6));
        }

        public PartialViewResult SelectPeriod(int year, int period)
        {
            var ret = GyglService.getPeriodBySearch(year, period);
            if (ret != null)
                return PartialView("GetPeriod", ret);
            return null;
        }
        /// <summary>
        /// ？？？？要改
        /// </summary>
        /// <param name="year"></param>
        /// <param name="period"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public PartialViewResult SelectArticle(int? year, int? period, int category)
        {
            return PartialView(ArticleService.getTitle(year, period, category));
        }


        /// <summary>
        /// 版权页
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public PartialViewResult GetPeriod(int id)
        {
            return PartialView(GyglService.getPeriodicalById(id));
        }

        /// <summary>
        /// 理事会
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public PartialViewResult GetPeriod2(int id)
        {
            return PartialView(GyglService.getPeriodicalById(id));
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

        public ActionResult GetCategoryList(int y, int p)
        {
            return Json(GyglCategoryService.getSearchCatalog(y,p),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDefaultCategoryList()
        {
            return Json(CategoryService.getCategoryList(),JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Search()
        {
            return PartialView();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
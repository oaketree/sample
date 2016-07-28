using Gygl.BLL.Magazine.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public async Task<PartialViewResult> CatalogAsync(int id)
        {
            var result = await GyglCategoryService.getCatalogByID(id);
            return PartialView(result);
        }

        public PartialViewResult Catalog(int id)
        {
            return CatalogAsync(id).Result;
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

        public PartialViewResult SelectYear()
        {
            return PartialView();
        }
        /// <summary>
        /// 页面内获取
        /// </summary>
        /// <param name="year"></param>
        /// <param name="period"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult getSelectYear(int? year, int? period, int page)
        {
            return Json(GyglService.getPeriodicalByYear(year, period, 6, page), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SelectArticle()
        {
            return PartialView();
        }
        /// <summary>
        /// 文章分页
        /// </summary>
        /// <param name="year"></param>
        /// <param name="period"></param>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult getSelectArticle(int? year, int? period, int? category, string key, int page)
        {
            return Json(ArticleService.getArticleByCategory(year, period, category, key, 20, page), JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult SelectPeriod(int year, int period)
        {
            var ret = GyglService.getPeriodBySearch(year, period);
            if (ret != null)
                return PartialView("GetPeriod", ret);
            return null;
        }



        /// <summary>
        /// 版权页
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public PartialViewResult GetPeriod(int? id)
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


        public async Task<JsonResult> GetPages(int aid)
        {
            await ArticleService.updateHit(aid);
            return Json(ImageService.getArticlePages(aid), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetArticleList(int pid)
        {
            var result = await ArticleService.getArticleList(pid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFirstPages(int pid)
        {
            return Json(ArticleService.getFirstPages(pid), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoryList(int y, int p)
        {
            return Json(GyglCategoryService.getSearchCatalog(y, p), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDefaultCategoryList()
        {
            return Json(CategoryService.getCategoryList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentPeriod()
        {
            return Json(GyglService.getCurrentPeriod(),JsonRequestBehavior.AllowGet);
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
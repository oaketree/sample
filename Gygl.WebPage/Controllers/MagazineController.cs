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


        //public async Task<PartialViewResult> CatalogAsync(int id)
        //{
        //    var result = await GyglCategoryService.getCatalogByID(id);
        //    return PartialView(result);
        //}

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
        /// <returns>改造</returns>
        public async Task<JsonResult> getSelectYear(int? year, int? period, int page)
        {
            var result = await GyglService.getPeriodicalByYear(year, period, 6, page);
            return Json(result, JsonRequestBehavior.AllowGet);
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
        /// <returns>改造</returns>
        public async Task<JsonResult> getSelectArticle(int? year, int? period, int? category, string key, int page)
        {
            var result = await ArticleService.getArticleByCategory(year, period, category, key, 20, page);
            return Json(result, JsonRequestBehavior.AllowGet);
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
            var result = await ImageService.getArticlePages(aid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetArticleList(int pid)
        {
            var result = await ArticleService.getArticleList(pid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetFirstPages(int pid)
        {
            var result = await ArticleService.getFirstPages(pid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCategoryList(int y, int p)
        {
            var result = await GyglCategoryService.getSearchCatalog(y, p);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDefaultCategoryList()
        {
            var result = await CategoryService.getCategoryList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCurrentPeriod()
        {
            var result = await GyglService.getCurrentPeriod();
            return Json(result,JsonRequestBehavior.AllowGet);
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
using Gygl.BLL.Magazine.Service;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
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
        public IArticleService ArticleService { get; set; }

        [Dependency]
        public ICategoryService CategoryService { get; set; }

        [Dependency]
        public ICommentService CommentService { get; set; }

        [OutputCache(Duration = 120)]
        public PartialViewResult Catalog()
        {
            //return PartialView(GyglCategoryService.getCatalogByID(id));
            return PartialView();
        }

        public async Task<JsonResult> GetCatalog(int id)
        {
            var result = await GyglCategoryService.getCatalogByID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 120)]
        public PartialViewResult RightNav()
        {
            return PartialView();
        }


        [OutputCache(Duration = 120)]
        public PartialViewResult Periodical()
        {
            //var ret = GyglService.getPeriodicalById(id);
            //if (ret != null)
            //    return PartialView(GyglService.getPeriodicalById(id));
            //else
            //    return null;
            return PartialView();
        }

        [OutputCache(Duration = 120, VaryByParam = "id")]
        public async Task<JsonResult> GetPeriod(int id)
        {
            var result = await GyglService.getPeriod(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetIp()
        {
            var result = Task.Run(() =>
            {
                return
                HttpContext.Request.UserHostAddress;
            });
            return Json(await result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getComment(int aid, int page)
        {
            var result = await CommentService.getCommentByArticle(aid, 3, page);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task smtComment(int aid, string message)
        {
            if (message.Trim() != string.Empty && message != null)
                await CommentService.smtComment(aid, message);
        }


        [OutputCache(Duration = 120)]
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

        [OutputCache(Duration = 120)]
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


        //public PartialViewResult SelectPeriod(int year, int period)
        //{
        //    var ret = GyglService.getPeriodBySearch(year, period);
        //    if (ret != null)
        //        return PartialView("CopyRight", ret);
        //    return null;
        //}
        public async Task<JsonResult> GetSelectPeriod(int year, int period)
        {
            var result = await GyglService.getPeriodBySearch(year, period);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 版权页CopyRight
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [OutputCache(Duration = 120)]
        public PartialViewResult CopyRight()
        {
            //return PartialView(GyglService.getPeriodicalById(id));
            return PartialView();
        }
        [OutputCache(Duration = 120, VaryByParam = "id")]
        public async Task<JsonResult> GetCopyRight(int? id)
        {
            var result = await GyglService.getPeriodicalById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 理事会
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OutputCache(Duration = 120)]
        public PartialViewResult Council()
        {
            return PartialView();
        }
        //用版权页获取
        //public async Task<JsonResult> GetCouncil(int? id)
        //{
        //    var result = await GyglService.getPeriodicalById(id);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [OutputCache(Duration = 120, VaryByParam = "aid")]
        public async Task<JsonResult> GetPages(int aid)
        {
            await ArticleService.updateHit(aid);
            //var result = await ImageService.getArticlePages(aid);
            var result = await ArticleService.getPages(aid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 120, VaryByParam = "pid")]
        public async Task<JsonResult> GetArticleList(int pid)
        {
            var result = await ArticleService.getArticleList(pid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 120, VaryByParam = "pid")]
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

        [OutputCache(Duration = 120)]
        public async Task<JsonResult> GetDefaultCategoryList()
        {
            var result = await CategoryService.getCategoryList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //首页的10条栏目
        [OutputCache(Duration = 120)]
        public async Task<JsonResult> GetCurrentPeriod()
        {
            var result = await GyglService.getCurrentPeriod();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 120)]
        public PartialViewResult Search()
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
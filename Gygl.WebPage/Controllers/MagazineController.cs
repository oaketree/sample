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
        

        public ActionResult Catalog(int id)
        {
            var a=GyglCategoryService.getCatalogByID(id);
            return Json(a,JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Article(int id)
        //{

        //}
    }
}
using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Magazine.Service
{
    public class GyglCategoryService: RepositoryBase<GyglCategory, WebDBContext>, IGyglCategoryService
    {
        [Dependency]
        public IArticleService ArticleService { get; set; }

        [Dependency]
        public IGyglService GyglService { get; set; }


        public List<CatalogViewModel> getCatalogByID(int gyglid)
        {
            var cvm = new List<CatalogViewModel>();
            var list = FindAll(n => n.GyglID == gyglid).Select(s=>new {
                Category=s.Category.Name,
                SortID=s.Category.SortID.Value,
                CategoryID= s.CategoryID.Value,
            });
            var sortedList=list.OrderBy(o => o.SortID);
            foreach (var item in sortedList)
            {
                cvm.Add(new CatalogViewModel
                {
                    Category = item.Category,
                    Title = ArticleService.getTitle(gyglid, item.CategoryID)
                });
            }
            return cvm;
        }
        //查询xx年x期的目录
        public object getSearchCatalog(int year, int period)
        {
            var gygl = GyglService.Get(n => n.Year == year && n.Period == period);
            if (gygl != null)
            {
                var list = FindAll(n => n.GyglID == gygl.ID).Select(s=>new {
                    CategoryID=s.CategoryID,
                    Category=s.Category.Name,
                    SortID=s.Category.SortID
                });
                return list.OrderBy(o=>o.SortID);
            }
            else
                return null;
        }
    }
}

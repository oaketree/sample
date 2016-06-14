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
        public List<CatalogViewModel> getCatalogByID(int gyglid)
        {
            var cvm = new List<CatalogViewModel>();
            var list = FindAll(n => n.GyglID == gyglid).Select(s=>new {
                Category=s.Category.Name,
                SortID=s.Category.SortID.Value,
                CategoryID= s.CategoryID.Value,
            });
            list.OrderBy(o => o.SortID);
            foreach (var item in list)
            {
                cvm.Add(new CatalogViewModel
                {
                    Category = item.Category,
                    Title = ArticleService.getTitle(gyglid, item.CategoryID)
                });
            }
            return cvm;
        }
    }
}

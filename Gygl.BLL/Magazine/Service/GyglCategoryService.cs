using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public class GyglCategoryService: RepositoryBase<GyglCategory, WebDBContext>, IGyglCategoryService
    {
        [Dependency]
        public IArticleService ArticleService { get; set; }

        [Dependency]
        public IGyglService GyglService { get; set; }

        //分布视图不支持异步
        public async Task<List<CatalogViewModel>> getCatalogByID(int gyglid)
        {
            var cvm = new List<CatalogViewModel>();
            //var list = FindAll(n => n.GyglID == gyglid).Select(s => new
            //{
            //    Category = s.Category.Name,
            //    SortID = s.Category.SortID.Value,
            //    CategoryID = s.CategoryID.Value,
            //});
            var fa = FindAll(n => n.GyglID == gyglid).OrderBy(o=>o.Category.SortID);
            var list = await FindAllAsync(fa, s => new {
                Category = s.Category.Name,
                //SortID = s.Category.SortID.Value,
                CategoryID = s.CategoryID.Value,
            });
            //var sortedList=list.OrderBy(o => o.SortID);
            foreach (var item in list)
            {
                cvm.Add(new CatalogViewModel
                {
                    Category = item.Category,
                    Title = await ArticleService.getTitle(gyglid, item.CategoryID)
                });
            }
            return cvm;
        }


        //查询xx年x期的目录
        public async Task<object> getSearchCatalog(int year, int period)
        {
            var gygl = await GyglService.GetAsync(n => n.Year == year && n.Period == period);
            if (gygl != null)
            {
                var fa = FindAll(n => n.GyglID == gygl.ID).OrderBy(o=>o.Category.SortID);
                var list =await FindAllAsync(fa, s => new
                {
                    CategoryID = s.CategoryID,
                    Category = s.Category.Name,
                    //SortID = s.Category.SortID
                });
                return list;
            }
            else
                return null;
        }
    }
}

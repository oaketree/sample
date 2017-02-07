using Core.Cache;
using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public class CategoryService: RepositoryBase<Category, WebDBContext>, ICategoryService
    {
        public async Task<object> getCategoryList()
        {
            var categoryList = CacheHelper.Get("categoryList");
            if (categoryList == null)
            {
                var fa = FindAll().OrderBy(o => o.SortID);
                categoryList = await FindAllAsync(fa, s => new
                {
                    Category = s.Name,
                    CategoryID = s.ID
                });
                CacheHelper.Set("categoryList", categoryList);
                
            }
            return categoryList;
        }
    }
}

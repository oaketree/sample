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
            var fa = FindAll().OrderBy(o => o.SortID);
            var li = await FindAllAsync(fa, s => new
            {
                Category = s.Name,
                CategoryID = s.ID
            });
            return li;
        }
    }
}

using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public  interface IGyglCategoryService : IRepository<GyglCategory>
    {
        Task<List<CatalogViewModel>> getCatalogByID(int gyglid);
        Task<object> getSearchCatalog(int year, int period);
    }
}

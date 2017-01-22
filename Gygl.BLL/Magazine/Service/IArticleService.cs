using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public interface IArticleService : IRepository<Article>
    {
        Task<IQueryable<TitleViewBase>> getTitle(int gyglid);
        Task<PageArticleViewModel> getArticleByCategory(int? year, int? period, int? category, string key, int pageSize, int page);
        Task<List<int>> getArticleList(int gyglId);
        Task updateHit(int aid);
        Task<object> getFirstPages(int pid);
        Task<object> getPages(int aid);
    }
}

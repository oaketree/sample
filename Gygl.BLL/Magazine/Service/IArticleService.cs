using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;

namespace Gygl.BLL.Magazine.Service
{
    public interface IArticleService : IRepository<Article>
    {
        List<TilteViewModel> getTitle(int gyglid, int categoryid);
        PageArticleViewModel getArticleByCategory(int? year, int? period, int category, int pageSize, int page);
        List<int> getArticleList(int gyglId);
    }
}

using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;

namespace Gygl.BLL.Magazine.Service
{
    public interface IArticleService : IRepository<Article>
    {
        List<TilteViewModel> getTitle(int gyglid, int categoryid);
        List<int> getArticleList(int gyglId);
    }
}

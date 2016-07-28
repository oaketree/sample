using Core.DAL;
using Gygl.BLL.News.ViewModels;
using Gygl.Contract.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.News.Service
{
    public interface INewsService : IRepository<New>
    {
        Task<IQueryable<NewsViewModel>> getNewsList(int column, int count);
        Task<PageNewsViewModel> getPagedNewsList(int pageSize, int page);
        Task<NewsViewModel> getNewsById(int id);
    }
}

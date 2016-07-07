using Core.DAL;
using Gygl.BLL.News.ViewModels;
using Gygl.Contract.News;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.News.Service
{
    public class NewsService : RepositoryBase<New, WebDBContext>, INewsService
    {
        public IQueryable<NewsViewModel> getNewsList(int column,int count)
        {
            var list = QueryEntity(n => n.ColumnID == column, o => o.RegDate, false).Take(count).Select(s => new NewsViewModel {
                ID = s.newsid,
                Title = s.Title,
                RegDate = s.RegDate
            });
            return list;
        }

        public PageNewsViewModel getPagedNewsList(int pageSize, int page)
        {
            var qe = QueryEntity(n => n.ColumnID == 101, o => o.RegDate, false);
            PageNewsViewModel pnvm = new PageNewsViewModel();
            var fbp = FindByPage(qe, pageSize, page).Select(s => new NewsViewModel
            {
                Title=s.Title,
                ID=s.newsid,
                RegDate=s.RegDate
            });
            pnvm.TotalItems = qe.Count();
            pnvm.CurrentPage = page;
            pnvm.ItemPerPage = pageSize;
            pnvm.Entity = fbp;
            return pnvm;
        }

        public NewsViewModel getNewsById(int id)
        {
            var n = Get(g => g.newsid == id);
            return new NewsViewModel
            {
                Title = n.Title,
                Content = n.Content,
                RegDate = n.RegDate
            };
        }
        
    }
}

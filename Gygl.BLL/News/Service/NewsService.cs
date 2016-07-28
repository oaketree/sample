using Core.DAL;
using Gygl.BLL.News.ViewModels;
using Gygl.Contract.News;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.News.Service
{
    public class NewsService : RepositoryBase<New, WebDBContext>, INewsService
    {
        public async Task<IQueryable<NewsViewModel>> getNewsList(int column,int count)
        {
            var fa = FindAll(n => n.ColumnID == column).OrderByDescending(o => o.RegDate).Take(count);
            var list = await FindAllAsync(fa, s => new NewsViewModel
            {
                ID = s.newsid,
                Title = s.Title,
                RegDate = s.RegDate
            });
            return list;
        }

        public async Task<PageNewsViewModel> getPagedNewsList(int pageSize, int page)
        {
            var qe = QueryEntity(n => n.ColumnID == 101, o => o.RegDate, false);
            PageNewsViewModel pnvm = new PageNewsViewModel();
            var fbp = FindByPageAsync(qe, pageSize, page, s => new NewsViewModel
            {
                Title = s.Title,
                ID = s.newsid,
                RegDate = s.RegDate
            });
            pnvm.TotalItems = await Count(qe);
            pnvm.CurrentPage = page;
            pnvm.ItemPerPage = pageSize;
            pnvm.Entity = await fbp;
            return pnvm;
        }

        public async Task<NewsViewModel> getNewsById(int id)
        {
            var n = await GetAsync(g => g.newsid == id);
            return new NewsViewModel
            {
                Title = n.Title,
                Content = n.Content,
                RegDate = n.RegDate
            };
        }
        
    }
}

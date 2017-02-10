using Core.DAL;
using Core.Log;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    
    public class ArticleService : RepositoryBase<Article, WebDBContext>, IArticleService
    {
        [Dependency]
        public IGyglService GyglService { get; set; }

        [Dependency]
        public ICategoryService CategoryService { get; set; }
        [Dependency]
        public IImageService ImageService { get; set; }

        //分布视图不支持异步
        //public async Task<IQueryable<TitleViewBase>> getTitle(int gyglid,int categoryid)
        //{
        //    var fa = FindAll(n => n.GyglID == gyglid && n.CategoryID == categoryid);
        //    var li = await FindAllAsync(fa, s => new TitleViewBase
        //    {
        //        Title = s.Title,
        //        Url = s.ID.ToString()
        //    });
        //    return li;
        //}
        public async Task<IQueryable<TitleViewBase>> getTitle(int gyglid)
        {
            var fa = FindAll(n => n.GyglID == gyglid);
            var li = await FindAllAsync(fa, s => new TitleViewBase
            {
                CategoryID=s.CategoryID.Value,
                Title=s.Title,
                Url = s.ID
            });
            return li;
        }

        //public async Task<List<int>> getArticleList(int gyglId)
        //{
        //    var fa = FindAll(n => n.GyglID == gyglId).OrderBy(o => o.Category.SortID).ThenBy(t => t.ID);
        //    var li = await FindAllAsync(fa, s => s.ID);
        //    return li.ToList();
        //}


        public async Task<PageArticleViewModel> getArticleByCategory(int? year, int? period, int? category,string key, int pageSize, int page)
        {
            bool p = false;
            IQueryable<Article> qe = null;
            PageArticleViewModel pif = new PageArticleViewModel();
            Expression<Func<Article, bool>> express = PredicateExtensions.True<Article>();
            if (category != null) {
                var categoryid=await CategoryService.GetAsync(g => g.ID == category);
                var name = categoryid.Name;
                pif.Category = name;
                express = express.And(n => n.CategoryID == category);
                p = true;
            }
            if (!string.IsNullOrEmpty(key)) {
                pif.Category = pif.Category + "   \"" + key + "\"";
                express = express.And(n => n.Title.Contains(key) || n.Keyword.Contains(key));
                p = true;
            }
            if (year != null) {
                express = express.And(n => n.Gygl.Year == year);
                p = true;
            }
            if (period != null) {
                express = express.And(n => n.Gygl.Period == period);
                p = true;
            }
            if (p == false)
            {
                pif.Category = "所有文章";
                qe = QueryEntity(null, o => o.RegDate, false);
            }
            else {
                qe = QueryEntity(express, o => o.RegDate, false);
            }
            var c = await Count(qe);
            pif.TotalItems = c;
            pif.CurrentPage = page;
            pif.ItemPerPage = pageSize;
            if (c!=0)
            {
                var fbp = FindByPageAsync(qe, pageSize, page, s => new TitleViewModel
                {
                    Title = s.Title,
                    Url = s.ID,
                    Author = s.Author,
                    Category = s.Category.Name,
                    Year = s.Gygl.Year.Value,
                    Period = s.Gygl.Period.Value,
                    GyglID = s.GyglID.Value
                });
                pif.Entity = await fbp;
                return pif;
            }
            else
                return null;
        }

        //public async Task updateHit(int aid)
        //{
        //    var ar = await GetAsync(n => n.ID == aid);
        //    ar.Hit = ar.Hit + 1;
        //    await UpdateAsync(ar);
        //}

        public async Task<object> getFirstPages(int pid)
        {
            var aid = await GetAsync(n => n.GyglID == pid);
            aid.Hit = aid.Hit + 1;
            await UpdateAsync(aid);
            var result = await ImageService.getMixedPages(pid, aid.ID,aid.Title);
            return result;
        }

        public async Task<object> getPages(int aid)
        {
            //await updateHit(aid);
            var pid = await GetAsync(aid);
            pid.Hit = pid.Hit + 1;
            await UpdateAsync(pid);
            var result = await ImageService.getMixedPages(pid.GyglID.Value, aid,pid.Title);
            return result;
        }


    }
}

using Core.DAL;
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

        public async Task<List<TilteViewModel>> getTitle(int gyglid,int categoryid)
        {
            //var li = FindAll(n => n.GyglID == gyglid && n.CategoryID == categoryid).Select(s=>new TilteViewModel
            //{
            //    Title=s.Title,
            //    Url=s.ID.ToString()
            //});
            var fa = FindAll(n => n.GyglID == gyglid && n.CategoryID == categoryid);
            var li = await FindAllAsync(fa, s => new TilteViewModel{
                Title = s.Title,
                Url = s.ID.ToString()
            });
            return li.ToList();
        }
        public async Task<List<int>> getArticleList(int gyglId)
        {
            //var li = FindAll(n => n.GyglID == gyglId).OrderBy(o => o.Category.SortID).ThenBy(t=>t.ID).Select(s => s.ID);
            //return li.ToList();
            var fa = FindAll(n => n.GyglID == gyglId).OrderBy(o => o.Category.SortID).ThenBy(t => t.ID);
            var li = await FindAllAsync(fa, s => s.ID);
            return li.ToList();
        }


        public PageArticleViewModel getArticleByCategory(int? year, int? period, int? category,string key, int pageSize, int page)
        {
            bool p = false;
            IQueryable<Article> qe = null;
            PageArticleViewModel pif = new PageArticleViewModel();
            Expression<Func<Article, bool>> express = PredicateExtensions.True<Article>();
            if (category != null) {
                pif.Category = CategoryService.Get(g => g.ID == category).Name;
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

            if (qe != null)
            {
                var fbp = FindByPage(qe, pageSize, page).Select(s => new TilteViewModel
                {
                    Title = s.Title,
                    Url = s.ID.ToString(),
                    Author = s.Author,
                    Category=s.Category.Name,
                    Year = s.Gygl.Year.Value,
                    Period = s.Gygl.Period.Value,
                    GyglID = s.GyglID.Value
                });
                pif.TotalItems = qe.Count();
                pif.CurrentPage = page;
                pif.ItemPerPage = pageSize;
                pif.Entity = fbp;
                return pif;
            }
            else
                return null;
        }

        public async Task updateHit(int aid)
        {
            var ar = Get(n => n.ID == aid);
            ar.Hit = ar.Hit + 1;
            await UpdateAsync(ar);
        }

        public object getFirstPages(int pid)
        {
            var id = Get(n => n.GyglID == pid).ID;
            return  ImageService.getArticlePages(id);
            //.Image.OrderBy(o => o.SortID).Select(s => new { url = s.ImageID });
        }


    }
}

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

        public List<TilteViewModel> getTitle(int gyglid,int categoryid)
        {
            var li = FindAll(n => n.GyglID == gyglid && n.CategoryID == categoryid).Select(s=>new TilteViewModel
            {
                Title=s.Title,
                Url=s.ID.ToString()
            });
            return li.ToList();
        }
        public List<int> getArticleList(int gyglId)
        {
            var li = FindAll(n => n.GyglID == gyglId).Select(s => s.ID);
            return li.ToList();
        }


        public PageArticleViewModel getArticleByCategory(int? year, int? period, int category, int pageSize, int page)
        {
            IQueryable<Article> qe = null;
            PageArticleViewModel pif = new PageArticleViewModel();
            pif.Category= CategoryService.Get(g => g.ID == category).Name;
            Expression<Func<Article, bool>> express = PredicateExtensions.True<Article>();
            express = express.And(n => n.CategoryID==category);
            if(year!=null)
                express = express.And(n => n.Gygl.Year==year);
            if(period!=null)
                express = express.And(n => n.Gygl.Period == period);
            qe = QueryEntity(express, o => o.RegDate, false);
            if (qe != null)
            {
                var fbp = FindByPage(qe, pageSize, page).Select(s => new TilteViewModel
                {
                    Title = s.Title,
                    Url = s.ID.ToString(),
                    Author=s.Author,
                    Year = s.Gygl.Year.Value,
                    Period = s.Gygl.Period.Value,
                    GyglID=s.GyglID.Value
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

    }
}

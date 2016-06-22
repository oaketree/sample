using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public class ArticleService : RepositoryBase<Article, WebDBContext>, IArticleService
    {
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
    }
}

using Core.DAL;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Magazine.Service
{
    public class ImageService: RepositoryBase<Image, WebDBContext>, IImageService
    {
        //[Dependency]
        //public IArticleService ArticleService { get; set; }
        public object getArticlePages(int aid)
        {
            var li = QueryEntity(n => n.ArticleID == aid, o => o.SortID, true).Select(s => new { url = s.ImageID });
            return li;
        }

        //public object getFirstPages(int pid)
        //{
        //    var aid=ArticleService.Get(n => n.GyglID == pid).ID;
        //    var li = QueryEntity(n => n.ArticleID == aid, o => o.SortID, true).Select(s => new { url = s.ImageID });
        //    return li;
        //}
    }
}

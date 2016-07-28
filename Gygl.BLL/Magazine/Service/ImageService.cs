using Core.DAL;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public class ImageService: RepositoryBase<Image, WebDBContext>, IImageService
    {
        public async Task<object> getArticlePages(int aid)
        {
            var fa = FindAll(n => n.ArticleID == aid).OrderBy(o => o.SortID);
            var list = await FindAllAsync(fa, s => new { url = s.ImageID });
            return list;
            //var li = QueryEntity(n => n.ArticleID == aid, o => o.SortID, true).Select(s => new { url = s.ImageID });
            //return li;
        }

    }
}

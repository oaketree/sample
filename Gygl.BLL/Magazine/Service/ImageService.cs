using Core.DAL;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Magazine.Service
{
    public class ImageService: RepositoryBase<Image, WebDBContext>, IImageService
    {
        public List<string> getArticleImage(int aid)
        {
            var li = QueryEntity(n => n.ArticleID == aid, o => o.SortID, true).Select(s=>s.ImageID);
            return li.ToList();
        }
    }
}

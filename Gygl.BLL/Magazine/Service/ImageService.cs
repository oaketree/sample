using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System;
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

        /// <summary>
        /// 和广告页面混合的页面
        /// </summary>
        /// <param name="gyglid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public async Task<List<ImgeViewModel>> getMixedPages(int gyglid,int aid)
        {
            var fa = FindAll(n => n.ArticleID == aid).OrderBy(o => o.SortID);
            var pageCount = await Count(fa);
            var listArticle = await FindAllAsync(fa, s => new ImgeViewModel {
                ImageID = s.ImageID,
                Url = "javascript:void(0)"
            });
            var listAd = await getAd(gyglid, pageCount);
            var endList = new List<ImgeViewModel>();
            //foreach (var item in listArticle)
            //{
            //    item
            //}
        }


        private async Task<IQueryable<ImgeViewModel>> getAd(int gyglid,int pageCount)
        {
            var fa = FindAll(n => n.GyglID == gyglid);
            var count = await Count(fa);
            int halfPageCount = pageCount / 2;
            var totalCount = 0;
            if (count <= halfPageCount)
                totalCount = count;
            else
                totalCount = halfPageCount;
            var newfa = FindAll(n => n.GyglID == gyglid).OrderBy(o => Guid.NewGuid()).Take(totalCount);
            var list =await FindAllAsync(newfa, s => new ImgeViewModel
            {
                ImageID = s.ImageID,
                Url = s.Url
            });
            return list;
        }

    }
}

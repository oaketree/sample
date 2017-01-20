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

        /// <summary>
        /// 和广告页面混合的页面
        /// </summary>
        /// <param name="gyglid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        //public async Task<List<ImageViewModel>> getMixedPages(int gyglid,int aid)
        //{
        //    var fa = FindAll(n => n.ArticleID == aid).OrderBy(o => o.SortID);
        //    var pageCount = await Count(fa);
        //    var listArticle = await FindAllAsync(fa, s => new ImageViewModel {
        //        ImageID = s.ImageID,
        //        Url = "",
        //        Location= "Page"
        //    });
        //    var listAd = await getAd(gyglid, pageCount);
        //    if (listAd == null)
        //        return listArticle.ToList();
        //    return crossPage(listArticle.ToList(), listAd.ToList());
        //}

        public async Task<ArticleImageViewModel> getMixedPages(int gyglid, int aid)
        {
            var aiv = new ArticleImageViewModel();
            aiv.ArticleID = aid;
            var fa = FindAll(n => n.ArticleID == aid).OrderBy(o => o.SortID);
            var pageCount = await Count(fa);
            var listArticle = await FindAllAsync(fa, s => new ImageViewModel
            {
                ImageID = s.ImageID,
                Url = "",
                Location = "Page"
            });
            var listAd = await getAd(gyglid, pageCount);
            if (listAd == null)
            {
                //aiv.AddRange(listArticle.ToList());
                aiv.ImageViews = listArticle.ToList();
            }
            else {
                //aiv.AddRange(crossPage(listArticle.ToList(), listAd.ToList()));
                aiv.ImageViews = crossPage(listArticle.ToList(), listAd.ToList());
            }   
            return aiv;
        }





        /// <summary>
        /// 广告文章交叉插入
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ad"></param>
        /// <returns></returns>
        private List<ImageViewModel> crossPage(List<ImageViewModel> page, List<ImageViewModel> ad)
        {
            int common = ad.Count();
            int max = page.Count();
            var endList = new ImageViewModel[common+max];
            int j = 0;
            for (int i = 0; i < common; i++)
            {
                endList[j++] = page[i];
                endList[j++] = ad[i];
            }
            for (int i = common; i < max; i++)
            {
                endList[j++] = page[i];
            }
            return endList.ToList();
        }
        /// <summary>
        /// 广告文章随机插入
        /// </summary>
        /// <param name="page"></param>
        /// <param name="ad"></param>
        /// <returns></returns>
        private List<ImageViewModel> crossPage2(List<ImageViewModel> page, List<ImageViewModel> ad)
        {
            int common = ad.Count();
            int max = page.Count();
            var endList = new ImageViewModel[common + max];
            int j = 0;
            for (int i = 0; i < common; i++)
            {
                endList[j++] = page[i];
                endList[j++] = ad[i];
            }
            for (int i = common; i < max; i++)
            {
                endList[j++] = page[i];
            }
            return endList.ToList();
        }


        private async Task<IQueryable<ImageViewModel>> getAd(int gyglid,int pageCount)
        {
            var fa = FindAll(n => n.GyglID == gyglid);
            var count = await Count(fa);
            if (count == 0)
                return null;
            int halfPageCount = pageCount / 3;
            var totalCount = 0;
            if (count <= halfPageCount)
                totalCount = count;
            else
                totalCount = halfPageCount;
            var newfa = FindAll(n => n.GyglID == gyglid).OrderBy(o => Guid.NewGuid()).Take(totalCount);
            var list = await FindAllAsync(newfa, s => new ImageViewModel
            {
                ImageID = s.ImageID,
                Url = s.Url,
                Location = "Ad"
            });
            return list;
        }

    }
}

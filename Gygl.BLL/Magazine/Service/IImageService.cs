using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public interface IImageService : IRepository<Image>
    {
        //Task<object> getArticlePages(int aid);
        //Task<List<ImageViewModel>> getMixedPages(int gyglid, int aid);
        Task<ArticleImageViewModel> getMixedPages(int gyglid, int aid,string title);
    }
}

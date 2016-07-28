using Core.DAL;
using Gygl.Contract.Magazine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public interface IImageService : IRepository<Image>
    {
        Task<object> getArticlePages(int aid);
        //object getFirstPages(int pid);
    }
}

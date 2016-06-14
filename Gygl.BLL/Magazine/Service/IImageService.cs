using Core.DAL;
using Gygl.Contract.Magazine;
using System.Collections.Generic;

namespace Gygl.BLL.Magazine.Service
{
    public interface IImageService : IRepository<Image>
    {
        List<string> getArticleImage(int aid);
    }
}

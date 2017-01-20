using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class ImageViewModel
    {
        public string ImageID { get; set; }
        public string Url { get; set; }
        public string Location { get; set; }
    }

    public class ArticleImageViewModel
    {
        public int ArticleID { get; set; }
        public List<ImageViewModel> ImageViews { get; set; }
    }
}

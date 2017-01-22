using Core.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class CatalogViewModel
    {
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public List<TitleViewBase> Title { get; set; }
    }

    public class TitleViewModel:TitleViewBase
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public int GyglID { get; set; }
    }
    public class TitleViewBase {
        public string Title { get; set; }
        public string Url { get; set; }
        public int CategoryID { get; set; }
    }
    public class PageArticleViewModel : PageInfo<TitleViewModel>
    {
        public string Category { get; set; }
    }
}

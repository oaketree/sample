using System.Collections.Generic;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class CatalogViewModel
    {
        public string Category { get; set; }
        public List<TilteViewModel> Title { get; set; }
        //public int SortID { get; set; }
    }

    public class TilteViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}

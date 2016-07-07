using System.Collections.Generic;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class CatalogViewModel
    {
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public List<TilteViewModel> Title { get; set; }
    }

    public class TilteViewModel
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public int GyglID { get; set; }
    }
}

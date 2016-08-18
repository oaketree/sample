using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class GyglViewModel:PeriodViewModel
    {
        public string CoverImage { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }
        public string Publish { get; set; }
        public string Council { get; set; }
        public string CopyRight { get; set; }
    }

    public class GyglTitleViewModel :PeriodViewModel{
        public IEnumerable<TitleViewModel> Title { get; set; }
    }

}

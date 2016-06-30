using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.ViewModels
{
    public class GyglViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public int TotalPeriod { get; set; }
        public string CoverImage { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }
    }
}

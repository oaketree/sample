using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Share
{
    public class GetNav
    {
        private int year;
        private int period;

        public GetNav(int year, int period)
        {
            this.year = year;
            this.period = period;
        }
        public Nav upNav()
        {
            if (period == 1)
            {
                return new Nav
                {
                    year = year - 1,
                    period = 6
                };
            }
            else {
                return new Nav
                {
                    year = year,
                    period = period-1
                };
            }
        }

        public Nav downNav()
        {
            if (period == 6)
            {
                return new Nav
                {
                    year = year + 1,
                    period = 1
                };
            }
            else {
                return new Nav
                {
                    year=year,
                    period=period+1
                };
            }
        }
    }
    public class Nav
    {
        public int year { get; set; }
        public int period { get; set; }
    }
}

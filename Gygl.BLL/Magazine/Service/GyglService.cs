using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.Contract.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.BLL.Magazine.Service
{
    public class GyglService:RepositoryBase<Periodical, WebDBContext>, IGyglService
    {
        public GyglViewModel getPeriodicalById(int pid)
        {
            var c = FindAll().Count();
            var p=Get(n => n.ID == pid);

            if (p != null)
            {
                int nav = p.ID;
                int up = 0;
                int down = 0;
                if (nav > 1)
                {
                    up = nav - 1;
                }
                else {
                    up = nav;
                }

                if (nav < c)
                {
                    down = nav + 1;
                }
                else {
                    down = nav;
                }
                return new GyglViewModel
                {
                    ID = p.ID,
                    Period = p.Period.Value,
                    TotalPeriod = p.TotalPeriod.Value,
                    Year = p.Year.Value,
                    CoverImage = p.CoverImage,
                    Up=up,
                    Down=down
                };
            }
            else
                return null;
        }
        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="year"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public PageInfo<Periodical> getPeriodicalByYear(int? year, int? period, int pageSize, int page)
        {
            if (year != null)
            {
                var qe = QueryEntity(n => n.Year == year, o => o.Period, true);
                if (qe != null)
                {
                    return FindByPage(qe, pageSize, page);
                }
                else
                    return null;
            }
            else
            {
                var qe = QueryEntity(n => n.Period == period, o => o.Year, false);
                if (qe != null)
                {
                    return FindByPage(qe, pageSize, page);
                }
                else
                    return null;
            }
        }

        public GyglViewModel getPeriodBySearch(int year, int period)
        {
            var c = FindAll().Count();
            var p= Get(n => n.Year == year && n.Period == period);
            if (p != null)
            {
                int nav = p.ID;
                int up = 0;
                int down = 0;
                if (nav > 1)
                {
                    up = nav - 1;
                }
                else
                {
                    up = nav;
                }

                if (nav < c)
                {
                    down = nav + 1;
                }
                else
                {
                    down = nav;
                }

                return new GyglViewModel
                {
                    ID = p.ID,
                    Period = p.Period.Value,
                    TotalPeriod = p.TotalPeriod.Value,
                    Year = p.Year.Value,
                    CoverImage = p.CoverImage,
                    Up=up,
                    Down=down
                };
            }
            else
                return null;
        }
    }
}

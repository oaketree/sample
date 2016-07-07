using Core.DAL;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.BLL.Share;
using Gygl.Contract.Magazine;
using System;
using System.Linq;

namespace Gygl.BLL.Magazine.Service
{
    public class GyglService : RepositoryBase<Periodical, WebDBContext>, IGyglService
    {
        private Tuple<int, int> getPid(int year, int period, int currentPid)
        {
            int up = currentPid;
            int down = currentPid;
            var getNav = new GetNav(year, period);
            var tempYear = getNav.upNav().year;
            var tempPeriod = getNav.upNav().period;
            var upPid = Get(n => n.Year ==tempYear && n.Period == tempPeriod);
            if (upPid != null)
                up = upPid.ID;
            tempYear = getNav.downNav().year;
            tempPeriod = getNav.downNav().period;
            var downPid = Get(n => n.Year == tempYear && n.Period == tempPeriod);
            if (downPid != null)
                down = downPid.ID;
            return new Tuple<int, int>(up,down);
        }

        public GyglViewModel getPeriodicalById(int? pid)
        {
            if (pid != null)
            {
                var p = Get(n => n.ID == pid);
                if (p != null)
                {
                    var gP = getPid(p.Year.Value, p.Period.Value, pid.Value);
                    return new GyglViewModel
                    {
                        ID = p.ID,
                        Period = p.Period.Value,
                        TotalPeriod = p.TotalPeriod.Value,
                        Year = p.Year.Value,
                        CoverImage = p.CoverImage,
                        Up = gP.Item1,
                        Down = gP.Item2
                    };
                }
                else
                    return null;
            }
            else
            {
                var p = QueryEntity(null, o => o.RegDate, false).FirstOrDefault();
                if (p != null)
                {
                    var gP = getPid(p.Year.Value, p.Period.Value, p.ID);
                    return new GyglViewModel
                    {
                        ID = p.ID,
                        Period = p.Period.Value,
                        TotalPeriod = p.TotalPeriod.Value,
                        Year = p.Year.Value,
                        CoverImage = p.CoverImage,
                        Up = gP.Item1,
                        Down = gP.Item2
                    };
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="year"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public PageGyglViewModel getPeriodicalByYear(int? year, int? period, int pageSize, int page)
        {
            IQueryable<Periodical> qe = null;
            PageGyglViewModel pif = new PageGyglViewModel();
            if (year != null)
            {
                qe = QueryEntity(n => n.Year == year, o => o.Period, true);
            }
            else
            {
                qe = QueryEntity(n => n.Period == period, o => o.Year, false);
            }
            if (qe != null)
            {
                var fbp = FindByPage(qe, pageSize, page).Select(s => new GyglViewModel
                {
                    ID = s.ID,
                    CoverImage = s.CoverImage,
                    Year = s.Year.Value,
                    Period = s.Period.Value
                });
                pif.TotalItems = qe.Count();
                pif.CurrentPage = page;
                pif.ItemPerPage = pageSize;
                pif.Entity = fbp;
                return pif;
            }
            else
            {
                return null;
            }
        }

        public GyglViewModel getPeriodBySearch(int year, int period)
        {
            var p = Get(n => n.Year == year && n.Period == period);
            if (p != null)
            {
                var gP = getPid(p.Year.Value, p.Period.Value, p.ID);
                return new GyglViewModel
                {
                    ID = p.ID,
                    Period = p.Period.Value,
                    TotalPeriod = p.TotalPeriod.Value,
                    Year = p.Year.Value,
                    CoverImage = p.CoverImage,
                    Up = gP.Item1,
                    Down = gP.Item2
                };
            }
            else
                return null;
        }


        public GyglTitleViewModel getCurrentPeriod()
        {
            var period = QueryEntity(null, o => o.RegDate, false).FirstOrDefault();
            var gtvm = new GyglTitleViewModel
            {
                ID = period.ID,
                Year = period.Year.Value,
                Period = period.Period.Value,
                Title = period.Article.OrderBy(o=>o.ID).Take(10).Select(s => new TilteViewModel {
                    Category = s.Category.Name,
                    Author=s.Author,
                    Title=s.Title,
                    Url=s.ID.ToString(),
                    GyglID=s.GyglID.Value
                })
            };
            return gtvm;
        }
    }
}

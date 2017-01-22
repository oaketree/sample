using Core.DAL;
using Core.Utility;
using Gygl.BLL.Magazine.ViewModels;
using Gygl.BLL.Share;
using Gygl.Contract.Magazine;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Gygl.BLL.Magazine.Service
{
    public class GyglService : RepositoryBase<Periodical, WebDBContext>, IGyglService
    {
        //[Dependency]
        //public IArticleService ArticleService { get; set; }

        private async Task<Tuple<int, int>> getPid(int year, int period, int currentPid)
        {
            int up = currentPid;
            int down = currentPid;
            var getNav = new GetNav(year, period);
            var tempYear = getNav.upNav().year;
            var tempPeriod = getNav.upNav().period;
            var upPid = await GetAsync(n => n.Year ==tempYear && n.Period == tempPeriod);
            if (upPid != null)
                up = upPid.ID;
            tempYear = getNav.downNav().year;
            tempPeriod = getNav.downNav().period;
            var downPid = await GetAsync(n => n.Year == tempYear && n.Period == tempPeriod);
            if (downPid != null)
                down = downPid.ID;
            return Tuple.Create(up,down);
        }

        //文章展示页
        public async Task<object> getPeriod(int? id)
        {
            Periodical p = null;
            if (id != null)
            {
                p =await GetAsync(id.Value);
            }
            else
            {
                var fa = FindAll(null).OrderByDescending(o => o.Year).ThenByDescending(t => t.Period);
                p = await GetAsync(fa);
            }
            return new PeriodViewModel
            {
                ID = p.ID,
                Period = p.Period.Value,
                Year = p.Year.Value,
                TotalPeriod=p.TotalPeriod.Value
            };
        }


        public async Task<GyglViewModel> getPeriodicalById(int? pid)
        {
            Periodical p = null;
            if (pid != null)
            {
                p = await GetAsync(pid.Value);
            }
            else {
                var fa = FindAll(null).OrderByDescending(o => o.Year).ThenByDescending(t=>t.Period);
                p = await GetAsync(fa);
            }
            if (p != null)
            {
                var nav = await getPid(p.Year.Value, p.Period.Value, p.ID);
                return new GyglViewModel
                {
                    ID = p.ID,
                    Period = p.Period.Value,
                    TotalPeriod = p.TotalPeriod.Value,
                    Year = p.Year.Value,
                    CoverImage = p.CoverImage,
                    Publish = p.Publish.Value.ToShortDateString(),
                    Council=p.Council,
                    CopyRight=p.CopyRight,
                    Up = nav.Item1,
                    Down = nav.Item2,
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
        public async Task<PageGyglViewModel> getPeriodicalByYear(int? year, int? period, int pageSize, int page)
        {
            //IQueryable<Periodical> qe = null;
            IQueryable<Periodical> fa = null;
            PageGyglViewModel pif = new PageGyglViewModel();
            if (year == null)
            {
                if (period == null)
                {
                    fa = FindAll().OrderByDescending(o => o.Year).ThenByDescending(o => o.Period);
                }
                else
                {
                    fa = FindAll(n => n.Period == period).OrderByDescending(o => o.Year);
                }
            }
            else {
                fa = FindAll(n => n.Year == year).OrderBy(o => o.Period);
            }

            var c = await Count(fa);
            pif.TotalItems = c;
            pif.CurrentPage = page;
            pif.ItemPerPage = pageSize;
            if (c!=0)
            {
                var fbp = FindByPageAsync(fa, pageSize, page, s => new GyglViewModel
                {
                    ID = s.ID,
                    CoverImage = s.CoverImage,
                    Year = s.Year.Value,
                    Period = s.Period.Value
                });
                pif.Entity =await fbp;
                return pif;
            }
            else
            {
                return null;
            }
        }

        public async Task<GyglViewModel> getPeriodBySearch(int year, int period)
        {
            var p = await GetAsync(n => n.Year == year && n.Period == period);
            if (p != null)
            {
                var nav = await getPid(p.Year.Value, p.Period.Value, p.ID);
                return new GyglViewModel
                {
                    ID = p.ID,
                    Period = p.Period.Value,
                    TotalPeriod = p.TotalPeriod.Value,
                    Year = p.Year.Value,
                    Council = p.Council,
                    CopyRight = p.CopyRight,
                    CoverImage = p.CoverImage,
                    Publish = p.Publish.Value.ToShortDateString(),
                    Up = nav.Item1,
                    Down = nav.Item2
                };
            }
            else
                return null;
        }

        //首页新闻列表
        public async Task<GyglTitleViewModel> getCurrentPeriod()
        {
            var fa = FindAll().OrderByDescending(o => o.Year).ThenByDescending(t => t.Period);
            var period = await GetAsync(fa);
            var tsk = Task.Run(() =>
            {
                var a=period.Article.OrderBy(o => o.ID).Take(10).Select(s => new TitleViewModel
                {
                    Category = s.Category.Name,
                    Author = s.Author,
                    Title = s.Title,
                    Url = s.ID,
                    GyglID = s.GyglID.Value
                });
                return a;
            });
            var gtvm = new GyglTitleViewModel
            {
                ID = period.ID,
                Year = period.Year.Value,
                Period = period.Period.Value,
                Title=await tsk
            };
            return gtvm;
        }

        
    }
}

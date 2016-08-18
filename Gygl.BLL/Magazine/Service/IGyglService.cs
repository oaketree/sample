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
    public interface IGyglService : IRepository<Periodical>
    {
        Task<object> getPeriod(int? id);
        Task<GyglViewModel> getPeriodicalById(int? pid);
        Task<PageGyglViewModel> getPeriodicalByYear(int? year, int? period, int pageSize, int page);
        Task<GyglViewModel> getPeriodBySearch(int year, int period);
        Task<GyglTitleViewModel> getCurrentPeriod();
    }
}

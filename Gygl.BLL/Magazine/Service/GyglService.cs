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
            var p=Get(n => n.ID == pid);
            return new GyglViewModel
            {
                ID = p.ID,
                Period = p.Period.Value,
                TotalPeriod = p.TotalPeriod.Value,
                Year = p.Year.Value
            };
        }
    }
}

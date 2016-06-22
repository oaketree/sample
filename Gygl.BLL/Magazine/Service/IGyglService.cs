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
        GyglViewModel getPeriodicalById(int pid);
    }
}

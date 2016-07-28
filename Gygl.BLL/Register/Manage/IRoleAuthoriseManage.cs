using Core.DAL;
using Gygl.Contract.Register;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public interface IRoleAuthoriseManage : IRepository<RoleAuthorise>
    {
        Task<IList<int>> GetAuthoriseByUser(Users usr);
    }
}

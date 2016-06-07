using Core.DAL;
using Gygl.Contract.Register;
using System.Collections.Generic;

namespace Gygl.BLL.Register.Manage
{
    public interface IRoleAuthoriseManage : IRepository<RoleAuthorise>
    {
        IList<int> GetAuthoriseByUser(Users usr);
    }
}

using Core.DAL;
using Gygl.Contract.Register;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public class RoleAuthoriseManage : RepositoryBase<RoleAuthorise, WebDBContext>, IRoleAuthoriseManage
    {
        public async Task<IList<int>> GetAuthoriseByUser(Users usr)
        {
            var roles = usr.Roles.Select(s => s.RoleID);
            var fa = FindAll(n => roles.Contains(n.RoleID));
            var result = await FindAllAsync(fa, s => s.AuthoriseID);
            return  result.ToList();
        }
    }
}

using Core.DAL;
using Gygl.Contract.Register;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Register.Manage
{
    public class RoleAuthoriseManage : RepositoryBase<RoleAuthorise, WebDBContext>, IRoleAuthoriseManage
    {
        //[Dependency]
        //public IUserRoleManage UserRoleManage { get; set; }
        public IList<int> GetAuthoriseByUser(Users usr)
        {
            var roles = usr.Roles.Select(s => s.RoleID);
            //var rid= UserRoleManage.FindAll(n => n.UserID == usr.UserID).Select(s=>s.RoleID);
            return  FindAll(n => roles.Contains(n.RoleID)).Select(s=>s.AuthoriseID).ToList();
        }
    }
}

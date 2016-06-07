using Core.DAL;
using Gygl.Contract.Register;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Gygl.BLL.Register.Manage
{
    public class RoleAuthoriseManage : RepositoryBase<RoleAuthorise, WebDBContext>, IRoleAuthoriseManage
    {
        [Dependency]
        public IUserRoleManage UserRoleManage { get; set; }
        public IList<int> GetAuthoriseByUser(Users usr)
        {
            var rid= UserRoleManage.FindAll(n => n.UserID == usr.UserID).Select(s=>s.RoleID);
           return  FindAll(n => rid.Contains(n.RoleID)).Select(s=>s.AuthoriseID).ToList();
        }
    }
}

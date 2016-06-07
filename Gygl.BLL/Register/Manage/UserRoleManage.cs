using Core.DAL;
using Gygl.Contract.Register;
using System.Linq;

namespace Gygl.BLL.Register.Manage
{
    public class UserRoleManage : RepositoryBase<UserRole, WebDBContext>, IUserRoleManage
    {
        public void InsertUserRoleByMagazine(Users usr)
        {
            var ur = FindAll(n => n.UserID == usr.UserID);
            if (ur.Count() == 0)
            {
                Insert(new UserRole
                {
                    UserID = usr.UserID,
                    RoleID = 4
                });
            }
            else
            {
                if (!ur.Any(n => n.RoleID == 4))
                {
                    Insert(new UserRole
                    {
                        UserID = usr.UserID,
                        RoleID = 4
                    });
                }
            }
        }
    }
}

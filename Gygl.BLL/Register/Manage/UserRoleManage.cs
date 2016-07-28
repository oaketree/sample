using Core.DAL;
using Gygl.Contract.Register;
using System.Linq;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public class UserRoleManage : RepositoryBase<UserRole, WebDBContext>, IUserRoleManage
    {
        public async Task InsertUserRoleByMagazine(Users usr)
        {
            var ur = FindAll(n => n.UserID == usr.UserID);
            var c = await Count(ur);
            if (c == 0)
            {
                await InsertAsync(new UserRole
                {
                    UserID = usr.UserID,
                    RoleID = 4
                });
            }
            else
            {
                var any = await Any(ur, n => n.RoleID == 4);
                if (!any)
                {
                    await InsertAsync(new UserRole
                    {
                        UserID = usr.UserID,
                        RoleID = 4
                    });
                }
            }
        }
    }
}

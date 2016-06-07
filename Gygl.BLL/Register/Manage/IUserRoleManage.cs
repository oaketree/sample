using Core.DAL;
using Gygl.Contract.Register;

namespace Gygl.BLL.Register.Manage
{
    public interface IUserRoleManage : IRepository<UserRole>
    {
        void InsertUserRoleByMagazine(Users usr);
    }
}

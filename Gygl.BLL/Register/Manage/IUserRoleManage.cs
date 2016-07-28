using Core.DAL;
using Gygl.Contract.Register;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public interface IUserRoleManage : IRepository<UserRole>
    {
       Task InsertUserRoleByMagazine(Users usr);
    }
}

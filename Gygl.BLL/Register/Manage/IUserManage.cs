using Core.DAL;
using Gygl.BLL.Register.ViewModels;
using Gygl.Contract.Register;
using System;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public interface IUserManage:IRepository<Users>
    {
        Task<bool> CkUserName(string username);
        Task<Tuple<bool, string>> Reg(RegViewModel rvm);
        Task<string> Activate(int uid, string code);
        Task<object> Login(string u, string p, bool auto);
        Task<object> LoginCheck();
        void LoginOut();
        Task<object> Forget(string u, string e);
        Task<PasswordViewModel> GetUser(int uid, string code);
        Task<PasswordViewModel> UpdatePass(PasswordViewModel pvm);
        Task<UserViewModel> GetUser();
        Task EditUser(UserViewModel uvm);
    }
}

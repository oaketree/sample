using Core.DAL;
using Gygl.BLL.Register.ViewModels;
using Gygl.Contract.Register;
using System;

namespace Gygl.BLL.Register.Manage
{
    public interface IUserManage:IRepository<Users>
    {
        bool CkUserName(string username);
        Tuple<bool, string> Reg(RegViewModel rvm);
        string Activate(int uid, string code);
        object Login(string u, string p, bool auto);
        object LoginCheck();
        void LoginOut();
        object Forget(string u, string e);
        PasswordViewModel GetUser(int uid, string code);
        PasswordViewModel UpdatePass(PasswordViewModel pvm);
        UserViewModel GetUser();
        void EditUser(UserViewModel uvm);
    }
}

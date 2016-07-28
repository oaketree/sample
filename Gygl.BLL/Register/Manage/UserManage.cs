using Core.Cache;
using Core.DAL;
using Core.Utility;
using Gygl.BLL.Register.ViewModels;
using Gygl.BLL.Share;
using Gygl.Contract.Register;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gygl.BLL.Register.Manage
{
    public class UserManage: RepositoryBase<Users,WebDBContext>,IUserManage
    {
        [Dependency]
        public IUserDetailManage UserDetailManage { get; set; }
        [Dependency]
        public IUserRoleManage UserRoleManage { get; set; }
        [Dependency]
        public IRoleAuthoriseManage RoleAuthoriseManage { get; set; }
        public async Task<bool> CkUserName(string username)
        {
            var result = await IsExistAsync(n => n.UserName == username);
            return !result;
        }

        public  async Task<Tuple<bool, string>> Reg(RegViewModel rvm)
        {
            var au = new Users
            {
                UserName = rvm.UserName,
                UserPassword = rvm.UserPassword,
                Password = Security.Sha256(rvm.UserPassword),
                LoginIP = Utils.GetIP(),
                UserLoginNum = 0,
                Valid = false,
                lastdate = DateTime.Now,
                ActiveCode = Guid.NewGuid().ToString("N")
            };
            Insert(au);
            await UserDetailManage.InsertAsync(new UserDetail
            {
                DisplayName = rvm.DisplayName,
                CompanyName = rvm.CompanyName,
                Email = rvm.Email,
                Address = rvm.Address,
                Tel = rvm.Tel,
                UserID = au.UserID,
            });
            await UserRoleManage.InsertAsync(new UserRole
            {
                UserID = au.UserID,
                RoleID = 4
            });
            return await SendRegisterMail(au.UserName, au.UserID.ToString(), au.ActiveCode, rvm.Email);
        }
        private Task<Tuple<bool,string>> SendRegisterMail(string username, string userid, string activecode, string email)
        {
            string sub = "《工业锅炉》杂志—邮箱验证";
            string[] b ={"<p>亲爱的{0}</p>",
"<p>您好！感谢您注册《工业锅炉》杂志会员！</p>",
"<p>请点击以下链接验证您的接收邮箱：<a href=\"http://gygl.boilerchina.cn/Registration/Activate?uid={1}&amp;code={2}\" target=\"_blank\">验证电子邮件地址</a></p>"};
            string body = string.Format(string.Join(",", b), username, userid, activecode);
            string emailFrom = "shgygl@126.com";
            string smtp = "smtp.126.com";
            string emailLoginName = "shgygl";
            string emailLoginpassword = "bjb142900";
            var e = new Email(sub, body, emailFrom, email);
            return 
            Task.Run(()=> {
                e.send(smtp, emailLoginName, emailLoginpassword);
                return e.Result;
            });
            
        }
        public async Task<string> Activate(int uid, string code)
        {
            string r = string.Empty;
            var u = Get(n => n.UserID == uid && n.ActiveCode == code);
            if (u != null)
            {
                if (!u.Valid.Value)
                {
                    u.Valid = true;
                    await UpdateAsync(u);
                    r = "恭喜您的帐号已经激活。";
                }
                else
                {
                    r = "帐号已经激活。";
                }
            }
            else
            {
                r = "用户信息不存在。";
            }
            return r;
        }

        public async Task<object> Login(string u, string p, bool auto)
        {
            var sp = Security.Sha256(p);
            var usr =await GetAsync(n => n.UserName == u && n.Password == sp);
            if (usr == null)
            {
                return new { status = 0, text = "用户名或密码错误！" };
            }
            else
            {
                if (usr.Valid == false)
                {
                    //注册邮箱激活处理
                    if (string.IsNullOrEmpty(usr.ActiveCode))
                    {
                        var ac = Guid.NewGuid().ToString("N");
                        usr.ActiveCode = ac;
                        await UpdateAsync(usr);
                        var s=await SendRegisterMail(usr.UserName, usr.UserID.ToString(), ac, usr.UserDetail.Email);
                        if(!s.Item1)
                            return new { status = 0, text = s.Item2 };
                    }
                    return new { status = 0, text = "该账号尚未激活，请至注册邮箱激活后登录！" };
                }
                else {
                    await UserRoleManage.InsertUserRoleByMagazine(usr);
                    usr.LoginIP = Utils.GetIP();
                    usr.UserLoginNum = usr.UserLoginNum + 1;
                    usr.lastdate = DateTime.Now;
                    usr.EntryPoint = EntryPoint.Magazine;
                    await UpdateAsync(usr);
                    SetSession(usr.UserID,usr.UserName,await RoleAuthoriseManage.GetAuthoriseByUser(usr));
                    if (auto)
                    {
                        SetCookie(usr.UserName, usr.Password);
                    }
                    return new { status = 1, text = string.Format("您好，{0}", usr.UserName) };
                }
            }
        }
        public async Task<object> LoginCheck()
        {
            if (SessionHelper.Get("AccessInfo") != null)
            {
                var login = (AccessInfo)SessionHelper.Get("AccessInfo");
                return new { status = 1, text = string.Format("您好，{0}", login.UserName) };
            }
            else
            {
                var u = CookieHelper.GetCookieValue("User");
                var p = CookieHelper.GetCookieValue("Password");
                if (u != string.Empty && p != string.Empty)
                {
                    var usr =await GetAsync(n => n.UserName == u && n.Password == p);
                    if (usr != null)
                    {
                        SetSession(usr.UserID, usr.UserName, await RoleAuthoriseManage.GetAuthoriseByUser(usr));
                        return new { status = 1, text = string.Format("您好，{0}", usr.UserName) };
                    }
                    else
                    {
                        CookieHelper.ClearCookie("User");
                        CookieHelper.ClearCookie("Password");
                        return new { status = 0 };
                    }
                }
                else {
                    return new { status = 0 };
                }

            }
        }
        public void LoginOut()
        {
            CookieHelper.ClearCookie("User");
            CookieHelper.ClearCookie("Password");
            SessionHelper.Del("AccessInfo");
        }

        public async Task<object> Forget(string u, string e)
        {
            var usr =await GetAsync(n => n.UserName == u && n.UserDetail.Email == e);
            if (usr == null)
            {
                return new { status = 0, text = "用户名或邮箱错误！" };
            }
            else
            {
                if (string.IsNullOrEmpty(usr.ActiveCode))
                {
                    var ac = Guid.NewGuid().ToString("N");
                    usr.ActiveCode = ac;
                    await UpdateAsync(usr);
                }
                return new { status = 1, uid = usr.UserID, code = usr.ActiveCode };
            }
        }

        public async Task<PasswordViewModel> GetUser(int uid, string code)
        {
            var user = await GetAsync(n => n.UserID == uid && n.ActiveCode == code);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new PasswordViewModel
                {
                    UserID = user.UserID,
                    UserName = user.UserName
                };
            }
        }

        public async Task<PasswordViewModel> UpdatePass(PasswordViewModel pvm)
        {
            var usr = await GetAsync(pvm.UserID);
            usr.UserPassword = pvm.UserPassword;
            usr.Password = Security.Sha256(pvm.UserPassword);
            await UpdateAsync(usr);
            return new PasswordViewModel
            {
                UserName = usr.UserName,
                UserPassword = usr.UserPassword,
            };
        }
        public async Task<UserViewModel> GetUser()
        {
            var login = (AccessInfo)SessionHelper.Get("AccessInfo");
            var uid = login.UserID;
            var user = await UserDetailManage.GetAsync(uid);
            return new UserViewModel
            {
                UserID = user.UserID,
                DisplayName = user.DisplayName,
                Email = user.Email,
                CompanyName = user.CompanyName,
                Address = user.Address,
                Tel = user.Tel
            };
        }
        public async Task EditUser(UserViewModel uvm)
        {
            var ud = await UserDetailManage.GetAsync(uvm.UserID);
            ud.DisplayName = uvm.DisplayName;
            ud.CompanyName = uvm.CompanyName;
            ud.Email = uvm.Email;
            ud.Address = uvm.Address;
            await UserDetailManage.UpdateAsync(ud);
        }


        private void SetSession(int userid,string username,IList<int> authorise)
        {
            SessionHelper.SetSession("AccessInfo", new AccessInfo
            {
                UserID = userid,
                UserName = username,
                Authorise = authorise
            });
        }

        private void SetCookie(string username, string password)
        {
            CookieHelper.SetCookie("User", username);
            CookieHelper.SetCookie("Password", password);
        }
    }
}

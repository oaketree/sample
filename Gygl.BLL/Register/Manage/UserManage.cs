using Core.Cache;
using Core.DAL;
using Core.Utility;
using Gygl.BLL.Register.ViewModels;
using Gygl.BLL.Share;
using Gygl.Contract.Register;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

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
        public bool CkUserName(string username)
        {
            return !IsExist(n => n.UserName == username);
        }

        public Tuple<bool, string> Reg(RegViewModel rvm)
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
            UserDetailManage.Insert(new UserDetail
            {
                DisplayName = rvm.DisplayName,
                CompanyName = rvm.CompanyName,
                Email = rvm.Email,
                Address = rvm.Address,
                Tel = rvm.Tel,
                UserID = au.UserID,
            });
            UserRoleManage.Insert(new UserRole
            {
                UserID = au.UserID,
                RoleID = 4
            });
            return SendRegisterMail(au.UserName, au.UserID.ToString(), au.ActiveCode, rvm.Email);
        }
        private Tuple<bool,string> SendRegisterMail(string username, string userid, string activecode, string email)
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
            e.send(smtp, emailLoginName, emailLoginpassword);
            return new Tuple<bool, string>(e.isSuccess,e.exMessage);
        }
        public string Activate(int uid, string code)
        {
            string r = string.Empty;
            var u = Get(n => n.UserID == uid && n.ActiveCode == code);
            if (u != null)
            {
                if (!u.Valid.Value)
                {
                    u.Valid = true;
                    Update(u);
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

        public object Login(string u, string p, bool auto)
        {
            var sp = Security.Sha256(p);
            var usr =Get(n => n.UserName == u && n.Password == sp);
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
                        Update(usr);
                        var s=SendRegisterMail(usr.UserName, usr.UserID.ToString(), ac, usr.UserDetail.Email);
                        if(!s.Item1)
                            return new { status = 0, text = s.Item2 };
                    }
                    return new { status = 0, text = "该账号尚未激活，请至注册邮箱激活后登录！" };
                }
                else {
                    UserRoleManage.InsertUserRoleByMagazine(usr);
                    usr.LoginIP = Utils.GetIP();
                    usr.UserLoginNum = usr.UserLoginNum + 1;
                    usr.lastdate = DateTime.Now;
                    usr.EntryPoint = EntryPoint.Magazine;
                    Update(usr);
                    SetSession(usr.UserID,usr.UserName,RoleAuthoriseManage.GetAuthoriseByUser(usr));
                    if (auto)
                    {
                        SetCookie(usr.UserName, usr.Password);
                    }
                    return new { status = 1, text = string.Format("您好，{0}", usr.UserName) };
                }
            }
        }
        public object LoginCheck()
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
                    var usr = Get(n => n.UserName == u && n.Password == p);
                    if (usr != null)
                    {
                        SetSession(usr.UserID, usr.UserName, RoleAuthoriseManage.GetAuthoriseByUser(usr));
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

        public object Forget(string u, string e)
        {
            var usr =Get(n => n.UserName == u && n.UserDetail.Email == e);
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
                    Update(usr);
                }
                return new { status = 1, uid = usr.UserID, code = usr.ActiveCode };
            }
        }

        public PasswordViewModel GetUser(int uid, string code)
        {
            var user = Get(n => n.UserID == uid && n.ActiveCode == code);
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

        public PasswordViewModel UpdatePass(PasswordViewModel pvm)
        {
            var usr = Get(pvm.UserID);
            usr.UserPassword = pvm.UserPassword;
            usr.Password = Security.Sha256(pvm.UserPassword);
            Update(usr);
            return new PasswordViewModel
            {
                UserName = usr.UserName,
                UserPassword = usr.UserPassword,
            };
        }
        public UserViewModel GetUser()
        {
            var login = (AccessInfo)SessionHelper.Get("AccessInfo");
            var uid = login.UserID;
            var user = UserDetailManage.Get(uid);
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
        public void EditUser(UserViewModel uvm)
        {
            var ud = UserDetailManage.Get(uvm.UserID);
            ud.DisplayName = uvm.DisplayName;
            ud.CompanyName = uvm.CompanyName;
            ud.Email = uvm.Email;
            ud.Address = uvm.Address;
            UserDetailManage.Update(ud);
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

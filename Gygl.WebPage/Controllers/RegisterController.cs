using Gygl.BLL.Register.Manage;
using Gygl.BLL.Register.ViewModels;
using Gygl.BLL.Share;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace Gygl.WebPage.Controllers
{
    public class RegisterController : Controller
    {
       [Dependency]
        public IUserManage UserManage { get; set; }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult JsonLogin(string u, string p, bool auto = false)
        {
            return Json(UserManage.Login(u, p, auto));
        }
        public ActionResult JsonLoginCheck()
        {
            return Json(UserManage.LoginCheck());
        }
        public void JsonLoginOut()
        {
            UserManage.LoginOut();
        }
        public ActionResult JsonForget(string u, string e)
        {
            return Json(UserManage.Forget(u, e));
        }
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reg(RegViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var a = UserManage.Reg(rvm);
                if(a.Item1)
                    return View("SendEmail", rvm);
                else
                    return View("ErrorEmail", (object)a.Item2);
            }
            return View();
        }
        public ActionResult Manage()
        {
            return View(UserManage.GetUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                UserManage.EditUser(uvm);
            }
            return View(uvm);
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult SendEmail(RegViewModel uvm)
        {
            return View(uvm);
        }
        public ActionResult ErrorEmail(string a)
        {
            return View(a);
        }
        public ActionResult Activate(int? uid, string code)
        {
            return View((object)UserManage.Activate(uid.Value, code));
        }

        public ActionResult ChangePassword(int? uid, string code)
        {
            var a = UserManage.GetUser(uid.Value, code);
            if (a == null)
            {
                return RedirectToAction("ForgetPassword");
            }
            else
            {
                return View(a);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                return View("ChangePasswordSuccess", UserManage.UpdatePass(pvm));
            }
            return View(pvm);
        }
        public ActionResult ChangePasswordSuccess(PasswordViewModel pvm)
        {
            return View(pvm);
        }
        public JsonResult CkUserName(string username)
        {
            return Json(UserManage.CkUserName(username.Trim()), JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoginJump(Jump model)
        {
            return View(model);
        }
    }
}
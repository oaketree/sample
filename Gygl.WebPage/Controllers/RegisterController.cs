using Gygl.BLL.Register.Manage;
using Gygl.BLL.Register.ViewModels;
using Gygl.BLL.Share;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
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

        public async Task<JsonResult> JsonLogin(string u, string p, bool auto = false)
        {
            var result = await UserManage.Login(u, p, auto);
            return Json(result);
        }
        public async Task<JsonResult> JsonLoginCheck()
        {
            var result = await UserManage.LoginCheck();
            return Json(result);
        }
        public void JsonLoginOut()
        {
            UserManage.LoginOut();
        }
        public async Task<JsonResult> JsonForget(string u, string e)
        {
            var result = await UserManage.Forget(u, e);
            return Json(result);
        }
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reg(RegViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                var a = await UserManage.Reg(rvm);
                if(a.Item1)
                    return View("SendEmail", rvm);
                else
                    return View("ErrorEmail", (object)a.Item2);
            }
            return View();
        }
        public async Task<ActionResult> Manage()
        {
            var result =await UserManage.GetUser();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                await UserManage.EditUser(uvm);
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
        public async Task<ActionResult> Activate(int? uid, string code)
        {
            var a = await UserManage.Activate(uid.Value, code);
            return View(a);
        }

        public async Task<ActionResult> ChangePassword(int? uid, string code)
        {
            var a = await UserManage.GetUser(uid.Value, code);
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
        public async Task<ActionResult> ChangePassword(PasswordViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                return View("ChangePasswordSuccess", await UserManage.UpdatePass(pvm));
            }
            return View(pvm);
        }
        public ActionResult ChangePasswordSuccess(PasswordViewModel pvm)
        {
            return View(pvm);
        }
        public async Task<JsonResult> CkUserName(string username)
        {
            var result = await UserManage.CkUserName(username.Trim());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoginJump(Jump model)
        {
            return View(model);
        }
    }
}
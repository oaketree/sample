using System.ComponentModel.DataAnnotations;

namespace Gygl.BLL.Register.ViewModels
{
    public class RegViewModel
    {
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{2}到{1}个字符")]
        [Required(ErrorMessage = "用户名为必填项目")]
        [Display(Name = "用户名")]
        [RegularExpression(@"^[a-zA-Z][A-Za-z0-9]*$", ErrorMessage = "{0}由数字和英文字母组成且以字母开头")]  //正则验证
        [System.Web.Mvc.Remote("CkUserName", "Register", ErrorMessage = "{0}已被注册")]
        public string UserName { get; set; }


        [StringLength(20, MinimumLength = 6, ErrorMessage = "{2}到{1}个字符")]
        [Required(ErrorMessage = "密码为必填项目")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "确认密码为必填项目")]
        [Compare("UserPassword", ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(4, MinimumLength = 2, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "真实姓名")]
        public string DisplayName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "电子邮箱为必填项目")]
        [Display(Name = "电子邮箱")]
        [RegularExpression(@"(\w)+(\.\w+)*@(\w)+((\.\w+)+)", ErrorMessage = "{0}格式不正确")]  //正则验证
        public string Email { get; set; }


        [StringLength(100)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }


        [StringLength(100)]
        [Display(Name = "联系电话")]
        public string Tel { get; set; }

        [StringLength(100)]
        [Display(Name = "联系地址")]
        public string Address { get; set; }
    }
}

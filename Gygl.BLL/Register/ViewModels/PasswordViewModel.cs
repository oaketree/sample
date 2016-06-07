using System.ComponentModel.DataAnnotations;

namespace Gygl.BLL.Register.ViewModels
{
    public class PasswordViewModel
    {
        public int UserID { get; set; }

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
    }
}

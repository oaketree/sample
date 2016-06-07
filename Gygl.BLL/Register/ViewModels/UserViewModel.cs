using System.ComponentModel.DataAnnotations;

namespace Gygl.BLL.Register.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }

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

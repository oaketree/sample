using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Contract.System
{
    [Table("Sys_User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [StringLength(20, MinimumLength = 2, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "真实姓名")]
        public string DisplayName { get; set; }


        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        [StringLength(50)]
        public string LoginIP { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<GroupUser> Groups { get; set; }
    }
}

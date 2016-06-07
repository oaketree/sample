using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.Register
{
    [Table("Tbl_User")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "原始密码")]
        public string UserPassword { get; set; }


        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "加密密码")]
        public string Password { get; set; }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int? UserLoginNum { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        public DateTime? lastdate { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        /// <summary>
        /// 登陆IP
        /// </summary>
        [StringLength(50)]
        public string LoginIP { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual UserDetail UserDetail { get; set; }

        public bool? Valid { get; set; }

        [StringLength(50)]
        public string ActiveCode { get; set; }

        public EntryPoint? EntryPoint { get; set; }

    }
    /// <summary>
    /// 登录入口
    /// </summary>
    public enum EntryPoint
    {
        Forum,
        Magazine
    }
}

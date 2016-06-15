using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_Permission")]
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<RolePermission> Roles { get; set; }
        public virtual ICollection<PermissionMenu> Menus { get; set; }
    }
}

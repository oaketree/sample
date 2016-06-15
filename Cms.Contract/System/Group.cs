using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_Group")]
    public class Group
    {
        [Key]
        public int GroupID { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public int? PID { get; set; }
        public virtual ICollection<GroupUser> Users { get; set; }
        public virtual ICollection<GroupRole> Roles { get; set; }
    }
}

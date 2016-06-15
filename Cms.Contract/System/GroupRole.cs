using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_GroupRole")]
    public class GroupRole
    {
        [Key]
        public int ID { get; set; }
        public int? GroupID { get; set; }
        public int? RoleID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Group Group { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_GroupUser")]
    public class GroupUser
    {
        [Key]
        public int ID { get; set; }

        public int? GroupID { get; set; }

        public int? UserID { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}

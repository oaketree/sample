using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_PermissionMenu")]
    public class PermissionMenu
    {
        [Key]
        public int ID { get; set; }

        public int? PermissionID { get; set; }

        public int? MenuID { get; set; }
        public virtual Permission Permission { get; set; }

        public virtual Menu Menu { get; set; }

    }
}

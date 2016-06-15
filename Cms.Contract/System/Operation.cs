using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_Operation")]
    public class Operation
    {
        [Key]
        public int OperationID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public int? PID { get; set; }
        public virtual ICollection<PermissionOperation> Permissions { get; set; }

    }
}

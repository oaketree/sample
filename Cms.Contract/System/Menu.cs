using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_Menu")]
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? PID { get; set; }
        public virtual ICollection<PermissionMenu> Permissions { get; set; }
    }
}

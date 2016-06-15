﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    [Table("Sys_RolePermission")]
    public class RolePermission
    {
        [Key]
        public int ID { get; set; }
        public int? RoleID { get; set; }
        public int? PermissionID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}

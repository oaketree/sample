using Core.Cache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.System
{
    public class WebDBContext : DbContext
    {
        public WebDBContext()
            : base(new GetConn("Sample").Conn())
        {

        }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupRole> GroupRole { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionMenu> PermissionMenu { get; set; }
        public virtual DbSet<PermissionOperation> PermissionOperation { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
    }
}

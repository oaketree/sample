using Core.Cache;
using System.Data.Entity;

namespace Gygl.Contract.Register
{
    public class WebDBContext : DbContext
    {
        public WebDBContext()
            : base(new GetConn("CMS").Conn())
        {

        }
        public virtual DbSet<Users> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Authorise> Authorise { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<RoleAuthorise> RoleAuthorise { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new DestinationMap());
            modelBuilder.Entity<UserDetail>().HasRequired(n => n.Users).WithOptional(n => n.UserDetail);

        }
    }
}

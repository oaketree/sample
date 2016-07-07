using Core.Cache;
using System.Data.Entity;

namespace Gygl.Contract.News
{
    public  class WebDBContext : DbContext
    {
        public WebDBContext()
            : base(new GetConn("CMS").Conn())
        {

        }

        public virtual DbSet<New> New { get; set; }
    }
}

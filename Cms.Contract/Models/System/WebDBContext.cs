using Core.Cache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.Models.System
{
    public class WebDBContext: DbContext
    {
        public WebDBContext()
            :base(new GetConn("Sample").Conn())
        {

        }

        public virtual DbSet<User> User { get; set; }
    }
}

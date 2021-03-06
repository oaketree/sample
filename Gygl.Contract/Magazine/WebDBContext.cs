﻿using Core.Cache;
using System.Data.Entity;

namespace Gygl.Contract.Magazine
{
    public class WebDBContext : DbContext
    {
        public WebDBContext()
            : base(new GetConn("CMS").Conn())
        {

        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Periodical> Gygl { get; set; }
        public virtual DbSet<Image> GyglImage { get; set; }
        public virtual DbSet<GyglCategory> GyglCategory { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
    }
}

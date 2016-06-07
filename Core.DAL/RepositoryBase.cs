using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DAL
{
    public abstract class RepositoryBase<T1, T2> : IRepository<T1>
        where T1 : class
        where T2 : DbContext, new()
    {
        private T2 dbContext = new T2();

        public DbSet<T1> MyDbSet
        {
            get
            {
                return dbContext.Set<T1>();
            }
        }
        public virtual T1 Get(int id)
        {
            var entry = MyDbSet.Find(id);
            return entry;
        }
        public virtual T1 Get(Expression<Func<T1, bool>> conditions = null)
        {
            if (conditions == null)
                return MyDbSet.AsNoTracking().FirstOrDefault();
            else
                return MyDbSet.AsNoTracking().FirstOrDefault(conditions);
        }
        public virtual bool IsExist(Expression<Func<T1, bool>> conditons)
        {
            var entry = MyDbSet.Where(conditons).AsNoTracking();
            return (entry.Any());
        }
        public virtual IQueryable<T1> FindAll(Expression<Func<T1, bool>> conditions = null)
        {
            if (conditions == null)
                return MyDbSet.AsNoTracking();
            else
                return MyDbSet.Where(conditions).AsNoTracking();
        }
        public virtual IQueryable<T1> QueryEntity<TOrderBy>
           (Expression<Func<T1, bool>> conditions,
            Expression<Func<T1, TOrderBy>> orderby,
            bool IsAsc)
        {
            IQueryable<T1> query = MyDbSet;
            if (conditions != null)
            {
                query = query.Where(conditions);
            }
            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            return query.AsNoTracking();
        }

        public virtual IQueryable<T1> FindList(IQueryable<T1> queryEntity,int take)
        {
            return queryEntity.Take(take);
        }

        public virtual IEnumerable<T1> QueryBySql(string sql,SqlParameter[] para)
        {
            return dbContext.Database.SqlQuery<T1>(sql, para);
        }

        public virtual PageInfo<T1> FindByPage(IQueryable<T1> QueryEntity, int PageSize, int page)
        {
            return new PageInfo<T1>
            {
                TotalItems = QueryEntity.Count(),
                CurrentPage = page,
                ItemPerPage = PageSize,
                Entity = QueryEntity.Skip((page - 1) * PageSize).Take(PageSize)
            };
        }
        public virtual void Update(T1 entity)
        {
            MyDbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        public virtual void Update()
        {
            dbContext.SaveChanges();
        }
        public virtual T1 Insert(T1 entity)
        {
            MyDbSet.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }
        public virtual void Delete(T1 entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
        public virtual int ExecuteSqlCommand(string sql, SqlParameter[] para)
        {
            return dbContext.Database.ExecuteSqlCommand(sql, para);
        }

    }
}

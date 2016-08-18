using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public virtual Task<T1> GetAsync(int id)
        {
            return Task.Run(()=> {
                var entry = MyDbSet.Find(id);
                return entry;
            });
        }

        public virtual T1 Get(Expression<Func<T1, bool>> conditions = null)
        {
            if (conditions == null)
                return MyDbSet.FirstOrDefault();
            else
                return MyDbSet.FirstOrDefault(conditions);
        }

        public virtual Task<T1> GetAsync(Expression<Func<T1, bool>> conditions = null)
        {
            return Task.Run(()=>{
                if (conditions == null)
                    return MyDbSet.FirstOrDefault();
                else
                    return MyDbSet.FirstOrDefault(conditions);
            });
        }
        public virtual Task<T1> GetAsync(IQueryable<T1> FindAll)
        {
            return Task.Run(() =>
            {
                return FindAll.FirstOrDefault();
            });
        }
        public virtual Task<int> Count(IQueryable<T1> FindAll)
        {
            return Task.Run(()=> {
                return FindAll.Count();
            });
        }

        public virtual Task<bool> Any(IQueryable<T1> FindAll, Expression<Func<T1, bool>> conditons)
        {
            return Task.Run(() => {
                return FindAll.Any(conditons);
            });
        }


        public virtual bool IsExist(Expression<Func<T1, bool>> conditons)
        {
            var entry = MyDbSet.Where(conditons).AsNoTracking();
            return (entry.Any());
        }

        public virtual Task<bool> IsExistAsync(Expression<Func<T1, bool>> conditons)
        {
            return Task.Run(() =>
            {
                var entry = MyDbSet.Where(conditons).AsNoTracking();
                return (entry.Any());
            });
        }
        public virtual IQueryable<T1> FindAll(Expression<Func<T1, bool>> conditions = null)
        {
            if (conditions == null)
                return MyDbSet.AsNoTracking();
            else
                return MyDbSet.Where(conditions).AsNoTracking();
        }
        public virtual Task<IQueryable<S>> FindAllAsync<S>(IQueryable<T1> FindAll, Expression<Func<T1, S>> select)
        {
            return Task.Run(()=> {
                return FindAll.Select(select);
            });
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


        public virtual Task<IQueryable<T1>> QueryEntityAsync<TOrderBy>
           (Expression<Func<T1, bool>> conditions,
            Expression<Func<T1, TOrderBy>> orderby,
            bool IsAsc)
        {
            return Task.Run(()=> {
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
            });
        }

        public virtual IEnumerable<T1> QueryBySql(string sql, SqlParameter[] para)
        {
            return dbContext.Database.SqlQuery<T1>(sql, para);
        }

        public virtual IQueryable<T1> FindByPage(IQueryable<T1> QueryEntity, int PageSize, int page)
        {
            return QueryEntity.Skip((page - 1) * PageSize).Take(PageSize);
        }

        public virtual Task<IQueryable<S>> FindByPageAsync<S>(IQueryable<T1> FindAll, int PageSize, int page, Expression<Func<T1, S>> select)
        {
            return Task.Run(()=> {
                return FindAll.Skip((page - 1) * PageSize).Take(PageSize).Select(select);
            });
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
        public virtual async Task UpdateAsync(T1 entity)
        {
            MyDbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public virtual T1 Insert(T1 entity)
        {
            MyDbSet.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public virtual async Task InsertAsync(T1 entity)
        {
            MyDbSet.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public virtual void Delete(T1 entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }

        public virtual async Task DeleteAsync(T1 entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }

        public virtual int ExecuteSqlCommand(string sql, SqlParameter[] para)
        {
            return dbContext.Database.ExecuteSqlCommand(sql, para);
        }

    }
}

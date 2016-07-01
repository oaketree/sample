using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DAL
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Get(Expression<Func<T, bool>> conditions = null);
        bool IsExist(Expression<Func<T, bool>> conditons);
        IQueryable<T> FindAll(Expression<Func<T, bool>> conditions = null);
        IQueryable<T> QueryEntity<TOrderBy>
           (Expression<Func<T, bool>> conditions,
            Expression<Func<T, TOrderBy>> orderby,
            bool IsAsc);
        IQueryable<T> FindList(IQueryable<T> queryEntity, int take);
        IEnumerable<T> QueryBySql(string sql, SqlParameter[] para);
        IQueryable<T> FindByPage(IQueryable<T> queryEntity, int PageSize, int page);
        void Update(T entity);
        void Update();
        T Insert(T entity);
        void Delete(T entity);
        int ExecuteSqlCommand(string sql, SqlParameter[] para);

    }
}

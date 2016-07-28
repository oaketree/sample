using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DAL
{
    public interface IRepository<T>
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(IQueryable<T> FindAll);
        T Get(Expression<Func<T, bool>> conditions = null);
        Task<T> GetAsync(Expression<Func<T, bool>> conditions);
        Task<int> Count(IQueryable<T> FindAll);
        Task<bool> Any(IQueryable<T> FindAll, Expression<Func<T, bool>> conditons);
        bool IsExist(Expression<Func<T, bool>> conditons);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> conditons);
        IQueryable<T> FindAll(Expression<Func<T, bool>> conditions);
        Task<IQueryable<S>> FindAllAsync<S>(IQueryable<T> FindAll, Expression<Func<T, S>> select);
        IQueryable<T> QueryEntity<TOrderBy>
           (Expression<Func<T, bool>> conditions,
            Expression<Func<T, TOrderBy>> orderby,
            bool IsAsc);
        Task<IQueryable<T>> QueryEntityAsync<TOrderBy>
           (Expression<Func<T, bool>> conditions,
            Expression<Func<T, TOrderBy>> orderby,
            bool IsAsc);
        IEnumerable<T> QueryBySql(string sql, SqlParameter[] para);
        IQueryable<T> FindByPage(IQueryable<T> queryEntity, int PageSize, int page);
        Task<IQueryable<S>> FindByPageAsync<S>(IQueryable<T> FindAll, int PageSize, int page, Expression<Func<T, S>> select);
        void Update(T entity);
        void Update();
        Task UpdateAsync(T entity);
        Task UpdateAsync();
        T Insert(T entity);
        Task InsertAsync(T entity);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        int ExecuteSqlCommand(string sql, SqlParameter[] para);

    }
}

using BlogProject.Core.DomainModels.BaseModels;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.IRepositories.BaseRepos
{
    public interface IBaseRepo<T> where T : IBaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<TResult> GetFilterEntityAsync<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null);
        Task<List<TResult>> GetFilterEntityListAsync<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null);

        public Task<int> AddAsync(T entity);
        public int Update(T Entity);
        public int Delete(T Entity);
    }
}

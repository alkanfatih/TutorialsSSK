using BlogProject.App.IRepositories.BaseRepos;
using BlogProject.Core.DomainModels.BaseModels;
using BlogProject.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories.BaseRepo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
           _table = context.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            _table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _table.AnyAsync(filter);
        }

        public int Delete(T Entity)
        {
            _table.Update(Entity);
            return _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return await _table.ToListAsync();
            else
                return await _table.Where(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TResult> GetFilterEntityAsync<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null)
        {
            IQueryable<T> query = _table;
            if (join != null)
            {
                query = join(query);
            }
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(select).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TResult>> GetFilterEntityListAsync<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null)
        {
            IQueryable<T> query = _table;
            if (join != null)
            {
                query = join(query);
            }
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
            {
                return await query.Select(select).ToListAsync();
            }
        }

        public int Update(T Entity)
        {
            _table.Update(Entity);
            return _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using repo.ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UOW.Domain.Repository;

namespace UOW.Service.Service
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> getAllAsync()
        {
            return await _context.Set<T>().ToListAsync(); 
        }
        public async Task<T> getByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> addAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public T update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> findWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            // Start with an unfiltered query
            IQueryable<T> query = _context.Set<T>();

            // Include navigation properties if specified
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Execute the query asynchronously
            return await query.ToListAsync();
        }

    }
}

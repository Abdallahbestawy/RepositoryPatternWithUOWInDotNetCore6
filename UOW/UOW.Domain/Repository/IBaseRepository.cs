using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UOW.Domain.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> getAllAsync();
        Task<T> getByIdAsync(int id);
        Task<T> addAsync(T entity);
        void Delete(T entity);
        T update(T entity);
        Task<IEnumerable<T>> findWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Domain.Commons;

namespace UserAuth.DataAccess.Repositories;

public interface IRepository<T> where T : Auditable
{
    ValueTask<T> InsertAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
    ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression);
    ValueTask<IEnumerable<T>> SelectAllAsEnumerableAsync(Expression<Func<T, bool>> expression = null);
    IQueryable<T> SelectAllAsQueryableAsync(Expression<Func<T, bool>> expression = null);
    ValueTask SaveChangesAsync();
}

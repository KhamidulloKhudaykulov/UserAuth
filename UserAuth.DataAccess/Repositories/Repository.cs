using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserAuth.DataAccess.Contexts;
using UserAuth.Domain.Commons;

namespace UserAuth.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this._context = context;
        this.set = context.Set<T>();
    }
    public async ValueTask<T> InsertAsync(T entity)
    {
        var result = await set.AddAsync(entity);
        return result.Entity;
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        return await Task.FromResult(set.Update(entity).Entity);
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression)
    {
        return await set.FirstOrDefaultAsync(expression);
    }

    public async ValueTask<IEnumerable<T>> SelectAllAsEnumerableAsync(Expression<Func<T, bool>> expression = null)
    {
        var result = expression is not null ? set.Where(expression) : set;
        return await Task.FromResult(result);
    }

    public IQueryable<T> SelectAllAsQueryableAsync(Expression<Func<T, bool>> expression = null)
    {
        var result = expression is not null ? set.Where(expression) : set;
        return result.AsQueryable();
    }

    public async ValueTask SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

using Crf.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crf.Infrastructure.Repositories.Common
{
  public class BaseRepository<TContext> where TContext : DbContext
  {
    protected TContext Context { get; private set; }

    public BaseRepository(TContext context)
    {
      Context = context;
    }

    #region Query

    protected IEnumerable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
    {
      return Context.Set<T>().Where(predicate).AsNoTracking().ToList();
    }

    protected T? Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
    {
      return Context.Set<T>().Where(predicate).FirstOrDefault();
    }

    #endregion Query

    #region Insert

    protected void Insert<T>(T entity) where T : class, IEntity
    {
      Context.Set<T>().Add(entity);
    }

    protected void Insert<T>(IEnumerable<T> entities) where T : class, IEntity
    {
      Context.Set<T>().AddRange(entities);
    }

    #endregion Insert

    #region Update

    protected void Update<T>(T entity) where T : class, IEntity
    {
      Context.Set<T>().Update(entity);
    }

    protected void Update<T>(IEnumerable<T> entities) where T : class, IEntity
    {
      Context.Set<T>().UpdateRange(entities);
    }

    #endregion Update

    #region Delete

    protected void Delete<T>(T entity) where T : class, IEntity
    {
      Context.Set<T>().Remove(entity);
    }

    protected void Delete<T>(IEnumerable<T> entities) where T : class, IEntity
    {
      Context.Set<T>().RemoveRange(entities);
    }

    protected void Delete<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
    {
      Context.Set<T>().RemoveRange(Context.Set<T>().Where(predicate));
    }

    #endregion Delete
  }
}
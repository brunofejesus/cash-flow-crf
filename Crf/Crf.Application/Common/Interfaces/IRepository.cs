using Crf.Domain.Commom;
using System.Linq.Expressions;

namespace Crf.Application.Common.Interfaces
{
  public interface IRepository<T> where T : BaseEntity
  {
    Task<IEnumerable<T>> GetAll();

    T? GetById(int id);

    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate);

    void Create(T entity);

    void Update(T entity);
  }
}
using Crf.Application.Common.Interfaces;
using Crf.Domain.Entities;
using System.Linq.Expressions;

namespace Crf.Application.Interfaces
{
  public interface IAccountRepository : IRepository<Account>
  {
    Task<IEnumerable<Activity>> GetActivitiesByFilter(Expression<Func<Activity, bool>> predicate);
  }
}
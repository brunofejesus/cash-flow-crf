using Crf.Application.Interfaces;
using Crf.Domain.Entities;
using Crf.Infrastructure.Persistence;
using Crf.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crf.Infrastructure.Repositories.CashFlow
{
	public class AccountRepository : BaseRepository<ApplicationDbContext>, IAccountRepository
	{
		public AccountRepository(ApplicationDbContext context) : base(context)
		{
		}

		public void Create(Account entity)
		{
			Insert(entity);
		}

		public async Task<IEnumerable<Activity>> GetActivitiesByFilter(Expression<Func<Activity, bool>> predicate)
		{
			return await Context
				.Activities
				.AsNoTracking()
				.Where(predicate)
				.ToListAsync();
		}

		public async Task<IEnumerable<Account>> GetAll()
		{
			return await Context
				.Accounts
				.AsNoTracking()
				.Include(x => x.Activities)
				.ToListAsync();
		}

		public async Task<Account?> GetByFilterAsync(Expression<Func<Account, bool>> predicate)
		{
			return await Context
				.Accounts
				.AsNoTracking()
				.Include(x => x.Activities)
				.Where(predicate)
				.FirstOrDefaultAsync();
		}

		public Account? GetById(int id)
		{
			return Get<Account>(x => x.Id == id);
		}

		public void Update(Account entity)
		{
			Context.ChangeTracker.Clear();
			base.Update(entity);
		}
	}
}
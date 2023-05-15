using Crf.Application.Interfaces;
using Crf.Domain.Entities;
using System.Linq.Expressions;

namespace Crf.Application.UnitTests.CashFlow.Simulators
{
	public class AccountRepositorySimulator : IAccountRepository
	{
		private List<Account> _accountsSimulator { get; init; } = new List<Account>();

		public AccountRepositorySimulator()
		{
			var account = new Account(
						name: "Main",
						number: "101",
						bank: "Unreal",
						userId: "1"
					);
			account.AddFinancialActivity(new Activity(
					typeActivity: Domain.Enums.ETypeActivity.Credit,
					description: "Sell products",
					amount: 100
				));
			_accountsSimulator.Add(account);
		}

		private IReadOnlyCollection<Account> Accounts => _accountsSimulator.ToArray();
		public bool CalledCreate { get; private set; }
		public bool CalledGetActivitiesByFilter { get; private set; }
		public bool CalledGetByFilterAsync { get; private set; }
		public bool CalledGetAll { get; private set; }
		public bool CalledGetById { get; private set; }
		public bool CalledUpdate { get; private set; }

		public void Create(Account entity)
		{
			CalledCreate = true;
		}

		public async Task<IEnumerable<Activity>> GetActivitiesByFilter(Expression<Func<Activity, bool>> predicate)
		{
			CalledGetActivitiesByFilter = true;
			return await Task.FromResult(_accountsSimulator.FirstOrDefault()?.Activities?.Where(predicate.Compile())?.AsEnumerable()!);
		}

		public async Task<IEnumerable<Account>> GetAll()
		{
			CalledGetAll = true;
			return await Task.FromResult(Accounts);
		}

		public async Task<Account?> GetByFilterAsync(Expression<Func<Account, bool>> predicate)
		{
			CalledGetByFilterAsync = true;
			return await Task.FromResult(_accountsSimulator?.Where(predicate.Compile())?.FirstOrDefault());
		}

		public Account? GetById(int id)
		{
			CalledGetById = true;
			return _accountsSimulator?.FirstOrDefault(a => a.Id == id);
		}

		public void Update(Account entity)
		{
			CalledUpdate = true;
			var itemIndex = _accountsSimulator.FindIndex(a => a.Id == entity.Id);
			_accountsSimulator[itemIndex] = entity;
		}
	}
}
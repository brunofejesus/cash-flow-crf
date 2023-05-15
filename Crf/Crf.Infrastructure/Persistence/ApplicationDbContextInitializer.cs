using Crf.Application.Common.Interfaces;
using Crf.Application.Interfaces;
using Crf.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Crf.Infrastructure.Persistence
{
	public class ApplicationDbContextInitializer
	{
		private readonly ILogger<ApplicationDbContextInitializer> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAccountRepository _repository;
		private readonly IUnitOfWork _unityOfWork;

		public ApplicationDbContextInitializer(
			ILogger<ApplicationDbContextInitializer> logger,
			ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			IAccountRepository repository,
			IUnitOfWork unityOfWork)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
			_repository = repository;
			_unityOfWork = unityOfWork;
		}

		public async Task InitialiseAsync()
		{
			try
			{
				if (_context.Database.IsNpgsql())
				{
					var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

					if (pendingMigrations.Any())
					{
						await _context.Database.MigrateAsync();
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while initialising the database.");
				throw;
			}
		}

		public async Task SeedAsync()
		{
			try
			{
				await TrySeedAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while seeding the database.");
				throw;
			}
		}

		public async Task TrySeedAsync()
		{
			var administrator = new ApplicationUser { UserName = "faustosilva@cashflow.com", Email = "faustosilva@cashflow.com" };

			if (_userManager.Users.All(u => u.UserName != administrator.UserName))
			{
				await _userManager.CreateAsync(administrator, "Administrator1!");
			}

			Domain.Entities.Account account = new(
						name: "Main",
						number: "101",
						bank: "Unreal",
						userId: administrator.Id
					);

			account.AddFinancialActivity(new Domain.Entities.Activity(
					typeActivity: Domain.Enums.ETypeActivity.Credit,
					description: "Sell products",
					amount: 100
				));

			await _context.Database.OpenConnectionAsync();
			((NpgsqlConnection)_context.Database.GetDbConnection()).ReloadTypes();
			var accounts = await _context.Accounts.ToListAsync();

			if (!accounts.Any())
			{
				_context.Accounts.Add(account);
				await _context.SaveChangesAsync();
			}
		}
	}
}
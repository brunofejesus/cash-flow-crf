using Crf.Application.CashFlow.Interfaces;
using Crf.Application.Common.Interfaces;
using Crf.Application.Interfaces;
using Crf.Infrastructure.CashFlow;
using Crf.Infrastructure.Identity;
using Crf.Infrastructure.Persistence;
using Crf.Infrastructure.Repositories;
using Crf.Infrastructure.Repositories.CashFlow;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crf.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(op =>
				op.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
						builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services
				.AddScoped<ApplicationDbContextInitializer>();

			services
				.AddScoped<IAccountRepository, AccountRepository>();

			services
				.AddTransient<IUnitOfWork, UnitOfWork>()
				.AddTransient<IFinancialReleaseControl, FinancialReleaseControl>()
				.AddTransient<IFinancialDailyConsolidatedReport, FinancialDailyConsolidatedReport>();

			return services;
		}
	}
}
using Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated;
using Crf.Application.Interfaces;
using Crf.Application.UnitTests.CashFlow.Simulators;
using MediatR;

namespace Crf.Application.UnitTests.CashFlow.GetReportOfDailyConsolidated
{
	[TestClass]
	public class GetReportOfDailyConsolidatedQueryTest : BaseTest
	{
		[TestMethod]
		public void ShouldGetReport_Successfully()
		{
			var command = new GetReportOfDailyConsolidatedQuery();

			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

			var mediator = Scope.GetRequiredService<ISender>();
			var result = mediator.Send(command).Result;

			Assert.IsTrue(result.Succeeded);
			Assert.IsTrue(repository.CalledGetByFilterAsync);
			Assert.IsTrue(repository.CalledGetActivitiesByFilter);
			Assert.IsTrue(result!.Report!.Itens!.Any());
		}

		[TestMethod]
		public void ShouldNotGetReportWithoutDate_Error()
		{
			var command = new GetReportOfDailyConsolidatedQuery()
			{
				Date = null!
			};

			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;
			Assert.ThrowsException<AggregateException>(() =>
			{
				var result = Scope.GetRequiredService<ISender>()!.Send(command).Result;
				Assert.IsFalse(result.Succeeded);
			});

			Assert.IsFalse(repository.CalledGetByFilterAsync);
			Assert.IsFalse(repository.CalledGetActivitiesByFilter);
		}

		protected override ServiceCollection RegisterServices(ServiceCollection services)
		{
			services.AddScoped<IAccountRepository, AccountRepositorySimulator>();
			services.AddLogging();
			return base.RegisterServices(services);
		}
	}
}
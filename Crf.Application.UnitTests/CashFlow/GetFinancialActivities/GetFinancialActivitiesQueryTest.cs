using Crf.Application.CashFlow.Queries.GetFinancialActivities;
using Crf.Application.Interfaces;
using Crf.Application.UnitTests.CashFlow.Simulators;
using MediatR;

namespace Crf.Application.UnitTests.CashFlow.GetFinancialActivities
{
  [TestClass]
  public class GetFinancialActivitiesQueryTest : BaseTest
  {
    [TestMethod]
    public void ShouldGetFinancialActivities_Successfully()
    {
      var command = new GetFinancialActivitiesQuery();
      var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

      var mediator = Scope.GetRequiredService<ISender>();
      var result = mediator.Send(command).Result;

      Assert.IsTrue(result.Succeeded);
      Assert.IsTrue(repository.CalledGetByFilterAsync);
      Assert.IsTrue(repository.CalledGetActivitiesByFilter);
      Assert.IsTrue(result.FinancialActivities.Any());
    }

    protected override ServiceCollection RegisterServices(ServiceCollection services)
    {
      services.AddScoped<IAccountRepository, AccountRepositorySimulator>();
      services.AddLogging();
      return base.RegisterServices(services);
    }
  }
}
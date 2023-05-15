using Crf.Application.CashFlow.CreateFinancialLaunch;
using Crf.Application.Common.Interfaces;
using Crf.Application.Interfaces;
using Crf.Application.UnitTests.CashFlow.Simulators;
using MediatR;

namespace Crf.Application.UnitTests.CashFlow.CreateFinancialLaunch
{
	[TestClass]
	public class CreateFinancialLaunchTest : BaseTest
	{
		[TestMethod]
		public void MustCreateFinancialLaunch_Successfully()
		{
			var command = new CreateFinancialLaunchCommand()
			{
				TypeActivity = "Credit",
				Amount = 100,
				Description = "Initial credit"
			};

			var unityOfWork = (UnitOfWorkSimulator)Scope.GetService<IUnitOfWork>()!;
			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

			var mediator = Scope.GetRequiredService<ISender>();
			var result = mediator.Send(command);

			Assert.IsTrue(result.Result.Succeeded);
			Assert.IsTrue(unityOfWork.CalledCommit);
			Assert.IsTrue(repository.CalledGetByFilterAsync);
			Assert.IsTrue(repository.CalledUpdate);
		}

		[TestMethod]
		public void ShouldNotCreateFinancialReleaseWithoutType_Error()
		{
			var command = new CreateFinancialLaunchCommand()
			{
				Amount = 100,
				Description = "Initial credit"
			};

			var unityOfWork = (UnitOfWorkSimulator)Scope.GetService<IUnitOfWork>()!;
			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

			Assert.ThrowsException<AggregateException>(() =>
			{
				var result = Scope.GetRequiredService<ISender>()!.Send(command).Result;
				Assert.IsFalse(result.Succeeded);
			});

			Assert.IsFalse(unityOfWork.CalledCommit);
			Assert.IsFalse(repository.CalledUpdate);
			Assert.IsFalse(repository.CalledGetByFilterAsync);
		}

		[TestMethod]
		public void ShouldNotCreateFinancialReleaseWithoutAmount_Error()
		{
			var command = new CreateFinancialLaunchCommand()
			{
				TypeActivity = "Credit",
				Description = "Initial credit"
			};

			var unityOfWork = (UnitOfWorkSimulator)Scope.GetService<IUnitOfWork>()!;
			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

			Assert.ThrowsException<AggregateException>(() =>
			{
				var result = Scope.GetRequiredService<ISender>()!.Send(command).Result;
				Assert.IsFalse(result.Succeeded);
			});

			Assert.IsFalse(unityOfWork.CalledCommit);
			Assert.IsFalse(repository.CalledUpdate);
			Assert.IsFalse(repository.CalledGetByFilterAsync);
		}

		[TestMethod]
		public void ShouldNotCreateFinancialReleaseWithoutDescription_Error()
		{
			var command = new CreateFinancialLaunchCommand()
			{
				TypeActivity = "Credit",
				Amount = 100,
			};

			var unityOfWork = (UnitOfWorkSimulator)Scope.GetService<IUnitOfWork>()!;
			var repository = (AccountRepositorySimulator)Scope.GetService<IAccountRepository>()!;

			Assert.ThrowsException<AggregateException>(() =>
			{
				var result = Scope.GetRequiredService<ISender>()!.Send(command).Result;
				Assert.IsFalse(result.Succeeded);
			});

			Assert.IsFalse(unityOfWork.CalledCommit);
			Assert.IsFalse(repository.CalledUpdate);
			Assert.IsFalse(repository.CalledGetByFilterAsync);
		}

		protected override ServiceCollection RegisterServices(ServiceCollection services)
		{
			services.AddScoped<IAccountRepository, AccountRepositorySimulator>();
			services.AddScoped<IUnitOfWork, UnitOfWorkSimulator>();
			services.AddLogging();
			return base.RegisterServices(services);
		}
	}
}
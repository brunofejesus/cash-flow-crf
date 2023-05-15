using Crf.Application.Common.Behaviours;
using FluentValidation;
using MediatR;

namespace Crf.Application.UnitTests
{
	public abstract class BaseTest
	{
		protected readonly IServiceProvider Scope;

		protected BaseTest()
		{
			Scope = RegisterServices(new ServiceCollection()).BuildServiceProvider().CreateScope().ServiceProvider;
		}

		protected virtual ServiceCollection RegisterServices(ServiceCollection services)
		{
			var assembly = AppDomain.CurrentDomain.GetAssemblies()
				.SingleOrDefault(assembly => assembly.GetName().Name == "Crf.Application");
			services.AddValidatorsFromAssembly(assembly);
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(assembly!);
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
				cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
			});
			return services;
		}
	}
}
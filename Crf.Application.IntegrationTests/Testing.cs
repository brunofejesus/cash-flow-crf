using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Respawn;

namespace Crf.Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
	private static WebApplicationFactory<Program> _factory = null!;
	private static IConfiguration _configuration = null!;
	private static IServiceScopeFactory _scopeFactory = null!;
	private static Respawner _checkpoint = null!;

	[OneTimeSetUp]
	public void RunBeforeAnyTests()
	{
		_factory = new CustomWebApplicationFactory();
		_scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
		_configuration = _factory.Services.GetRequiredService<IConfiguration>();

		_checkpoint = Respawner.CreateAsync(_configuration.GetConnectionString("DefaultConnection")!, new RespawnerOptions
		{
			TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
		}).GetAwaiter().GetResult();
	}

	public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
	{
		using var scope = _scopeFactory.CreateScope();

		var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

		return await mediator.Send(request);
	}

	public static async Task SendAsync(IBaseRequest request)
	{
		using var scope = _scopeFactory.CreateScope();

		var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

		await mediator.Send(request);
	}
}
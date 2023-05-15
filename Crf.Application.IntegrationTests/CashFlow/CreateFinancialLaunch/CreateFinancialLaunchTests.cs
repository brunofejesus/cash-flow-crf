using Crf.Application.CashFlow.CreateFinancialLaunch;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace Crf.Application.IntegrationTests.CashFlow.CreateFinancialLaunch
{
	using static Testing;

	public class CreateFinancialLaunchTests : BaseTestFixture
	{
		[Test]
		public async Task ShouldRequireMandatoryFields()
		{
			var command = new CreateFinancialLaunchCommand();
			await FluentActions.Invoking(() =>
						SendAsync(command)).Should().ThrowAsync<ValidationException>();
		}
	}
}
using Crf.Application.CashFlow.Models;
using Crf.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crf.Application.CashFlow.Queries.GetFinancialActivities
{
	public class GetFinancialActivitiesQueryHandler : IRequestHandler<GetFinancialActivitiesQuery, FinancialActivitiesResult>
	{
		private readonly ILogger<GetFinancialActivitiesQueryHandler> _logger;
		private readonly IAccountRepository _repository;

		public GetFinancialActivitiesQueryHandler(
			ILogger<GetFinancialActivitiesQueryHandler> logger,
			IAccountRepository repository)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<FinancialActivitiesResult> Handle(GetFinancialActivitiesQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Running GetFinancialLaunchesQuery...");

			var mainAccount = await _repository.GetByFilterAsync(x => x.Name.ToLower().Equals("main"));

			if (mainAccount is null)
			{
				return await Task.FromResult(FinancialActivitiesResult.Error(new List<string>()
				{
					"No account was found"
				}));
			}

			var accountActivities = await _repository.GetActivitiesByFilter(
				x => x.AccountId == mainAccount.Id);

			var accountActivitiesOrdered = accountActivities?.OrderBy(ri => ri.Created)?.ToList();

			return await Task.FromResult(
					FinancialActivitiesResult.Success(
							financialActivities: accountActivitiesOrdered?
								.Select(aa => new FinancialActivityDto()
								{
									TypeActivity = aa?.TypeActivity,
									Amount = aa?.Amount,
									Description = aa?.Description,
									Created = aa?.Created
								}).ToList()
						)
				);
		}
	}
}
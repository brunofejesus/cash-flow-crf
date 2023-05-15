using Crf.Application.CashFlow.Models;
using Crf.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated
{
	public class GetReportOfDailyConsolidatedQueryHandler : IRequestHandler<GetReportOfDailyConsolidatedQuery, FinancialReportResult>
	{
		private readonly ILogger<GetReportOfDailyConsolidatedQueryHandler> _logger;
		private readonly IAccountRepository _repository;

		public GetReportOfDailyConsolidatedQueryHandler(
			ILogger<GetReportOfDailyConsolidatedQueryHandler> logger,
			IAccountRepository repository)
		{
			_logger = logger;
			_repository = repository;
		}

		public async Task<FinancialReportResult> Handle(GetReportOfDailyConsolidatedQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Running GetReportOfDailyConsolidatedQuery...");

			var account = await _repository.GetByFilterAsync(x => x.Name.ToLower().Equals("main"));

			if (account is null)
			{
				return await Task.FromResult(FinancialReportResult.Error(new List<string>()
				{
					"No account was found"
				}));
			}

			var reportItens = await _repository.GetActivitiesByFilter(
				x => x.AccountId == account.Id &&
				x.Created.Date == request.Date!.Value.Date.ToUniversalTime());

			var reportItensOrdered = reportItens?.OrderBy(ri => ri.Created)?.ToList();

			return await Task.FromResult(
				FinancialReportResult.Success(
					new FinancialReportDto()
					{
						Itens = reportItensOrdered?
						.Select(ri => new FinancialActivityDto()
						{
							TypeActivity = ri?.TypeActivity,
							Amount = ri?.Amount,
							Description = ri?.Description,
							Created = ri?.Created
						})?.ToList(),
						AmountInflows = reportItensOrdered?
						.Where(ri => ri.TypeActivity == Domain.Enums.ETypeActivity.Credit)?
						.Sum(ri => ri.Amount),
						AmountOutflows = reportItensOrdered?
						.Where(ri => ri.TypeActivity == Domain.Enums.ETypeActivity.Debit)?
						.Sum(ri => ri.Amount),
					}));
		}
	}
}
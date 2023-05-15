using Crf.Application.CashFlow.Models;
using MediatR;

namespace Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated
{
	public class GetReportOfDailyConsolidatedQuery : IRequest<FinancialReportResult>
	{
		public DateTime? Date { get; set; } = DateTime.UtcNow;
	}
}
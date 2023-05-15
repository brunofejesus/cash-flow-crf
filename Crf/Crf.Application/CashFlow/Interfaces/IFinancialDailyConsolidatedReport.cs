using Crf.Application.CashFlow.Models;
using Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated;

namespace Crf.Application.CashFlow.Interfaces
{
	public interface IFinancialDailyConsolidatedReport
	{
		Task<FinancialReportResult> GetDailyConsolidatedReport(GetReportOfDailyConsolidatedQuery query);
	}
}
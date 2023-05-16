using Crf.Application.CashFlow.CreateFinancialLaunch;
using Crf.Application.CashFlow.Models;
using Crf.Application.CashFlow.Queries.GetFinancialActivities;
using Crf.Application.Common.Models;

namespace Crf.Application.CashFlow.Interfaces
{
	public interface IFinancialReleaseControl
	{
		Task<Result> AddFinancialLaunch(CreateFinancialLaunchCommand command);

		Task<FinancialActivitiesResult> GetFinancialActivities(GetFinancialActivitiesQuery query);
	}
}
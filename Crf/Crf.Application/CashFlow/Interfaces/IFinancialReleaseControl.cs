using Crf.Application.CashFlow.CreateFinancialLaunch;
using Crf.Application.Common.Models;

namespace Crf.Application.CashFlow.Interfaces
{
	public interface IFinancialReleaseControl
	{
		Task<Result> AddFinancialLaunch(CreateFinancialLaunchCommand command);
	}
}
using Crf.Application.CashFlow.Models;
using MediatR;

namespace Crf.Application.CashFlow.Queries.GetFinancialActivities
{
	public class GetFinancialActivitiesQuery : IRequest<FinancialActivitiesResult>
	{
	}
}
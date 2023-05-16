namespace Crf.Application.CashFlow.Models
{
	public class FinancialActivitiesResult
	{
		public bool Succeeded { get; init; }
		public List<string> Errors { get; init; }

		public List<FinancialActivityDto> FinancialActivities { get; set; }

		private FinancialActivitiesResult(bool succeeded, IEnumerable<string> errors)
		{
			Succeeded = succeeded;
			Errors = errors.ToList();
			FinancialActivities = new List<FinancialActivityDto>();
		}

		private FinancialActivitiesResult(bool succeeded, IEnumerable<string> errors, List<FinancialActivityDto>? financialActivities)
		{
			FinancialActivities = new List<FinancialActivityDto>();
			Succeeded = succeeded;
			Errors = errors.ToList();
			if (financialActivities is not null && financialActivities.Any())
			{
				FinancialActivities.AddRange(financialActivities);
			}
		}

		public static FinancialActivitiesResult Success(List<FinancialActivityDto>? financialActivities)
		{
			return new FinancialActivitiesResult(true, Array.Empty<string>(), financialActivities);
		}

		public static FinancialActivitiesResult Error(IEnumerable<string> errors)
		{
			return new FinancialActivitiesResult(false, errors);
		}
	}
}
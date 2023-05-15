using FluentValidation;

namespace Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated
{
  public class GetReportOfDailyConsolidatedQueryValidator : AbstractValidator<GetReportOfDailyConsolidatedQuery>
  {
    public GetReportOfDailyConsolidatedQueryValidator()
    {
      RuleFor(x => x.Date)
        .NotEmpty().WithMessage("Date is required");
    }
  }
}
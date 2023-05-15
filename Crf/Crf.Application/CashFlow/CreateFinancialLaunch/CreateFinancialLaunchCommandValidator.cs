using Crf.Domain.Enums;
using FluentValidation;

namespace Crf.Application.CashFlow.CreateFinancialLaunch
{
  public class CreateFinancialLaunchCommandValidator : AbstractValidator<CreateFinancialLaunchCommand>
  {
    public CreateFinancialLaunchCommandValidator()
    {
      RuleFor(x => x.TypeActivity)
        .NotEmpty().WithMessage("Type Activity is required")
        .IsEnumName(typeof(ETypeActivity), false).WithMessage("Select a valid Type Activity");

      RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Description is required");

      RuleFor(x => x.Amount)
        .NotEmpty().WithMessage("Amount is required")
        .GreaterThan(0).WithMessage("Amount is required");
    }
  }
}
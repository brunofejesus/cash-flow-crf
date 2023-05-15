using Crf.Domain.Enums;

namespace Crf.Application.CashFlow.Models
{
  public class FinancialActivityDto
  {
    public ETypeActivity? TypeActivity { get; init; }
    public string? Description { get; init; }
    public decimal? Amount { get; init; }
    public DateTime? Created { get; init; }
  }
}
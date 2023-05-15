namespace Crf.Application.CashFlow.Models
{
  public class FinancialReportDto
  {
    public List<FinancialActivityDto>? Itens { get; init; } = new();
    public decimal? AmountInflows { get; init; }
    public decimal? AmountOutflows { get; init; }
  }
}
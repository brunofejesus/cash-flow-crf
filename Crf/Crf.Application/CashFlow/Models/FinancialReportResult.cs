namespace Crf.Application.CashFlow.Models
{
  public class FinancialReportResult
  {
    public bool Succeeded { get; init; }
    public List<string> Errors { get; init; }
    public FinancialReportDto? Report { get; set; } = new();

    private FinancialReportResult(bool succeeded, IEnumerable<string> errors)
    {
      Succeeded = succeeded;
      Errors = errors.ToList();
    }

    private FinancialReportResult(bool succeeded, IEnumerable<string> errors, FinancialReportDto report)
    {
      Succeeded = succeeded;
      Errors = errors.ToList();
      Report = report;
    }

    public static FinancialReportResult Success()
    {
      return new FinancialReportResult(true, Array.Empty<string>());
    }

    public static FinancialReportResult Success(FinancialReportDto report)
    {
      return new FinancialReportResult(true, Array.Empty<string>(), report);
    }

    public static FinancialReportResult Error(IEnumerable<string> errors)
    {
      return new FinancialReportResult(false, errors);
    }
  }
}
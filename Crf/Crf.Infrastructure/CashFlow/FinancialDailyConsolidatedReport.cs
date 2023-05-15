using Crf.Application.CashFlow.Interfaces;
using Crf.Application.CashFlow.Models;
using Crf.Application.CashFlow.Queries.GetReportOfDailyConsolidated;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crf.Infrastructure.CashFlow
{
  public class FinancialDailyConsolidatedReport : IFinancialDailyConsolidatedReport
  {
    private readonly ILogger<FinancialDailyConsolidatedReport> _logger;
    private readonly ISender? _mediator;

    public FinancialDailyConsolidatedReport(
      ILogger<FinancialDailyConsolidatedReport> logger,
      ISender? mediator)
    {
      _logger = logger;
      _mediator = mediator;
    }

    public async Task<FinancialReportResult> GetDailyConsolidatedReport(GetReportOfDailyConsolidatedQuery query)
    {
      try
      {
        return await _mediator!.Send(query);
      }
      catch (ValidationException ex)
      {
        return FinancialReportResult.Error(ex.Errors.Select(x => x.ErrorMessage).ToList());
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error GetDailyConsolidatedReport");
        return FinancialReportResult.Error(new List<string>() { "Internal Error" });
      }
    }
  }
}
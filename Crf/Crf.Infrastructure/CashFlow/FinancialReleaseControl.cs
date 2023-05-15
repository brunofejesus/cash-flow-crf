using Crf.Application.CashFlow.CreateFinancialLaunch;
using Crf.Application.CashFlow.Interfaces;
using Crf.Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crf.Infrastructure.CashFlow
{
  public class FinancialReleaseControl : IFinancialReleaseControl
  {
    private readonly ILogger<FinancialReleaseControl> _logger;
    private readonly ISender? _mediator;

    public FinancialReleaseControl(
      ILogger<FinancialReleaseControl> logger,
      ISender? mediator)
    {
      _mediator = mediator;
      _logger = logger;
    }

    public async Task<Result> AddFinancialLaunch(CreateFinancialLaunchCommand command)
    {
      try
      {
        return await _mediator!.Send(command);
      }
      catch (ValidationException ex)
      {
        return Result.Error(ex.Errors.Select(x => x.ErrorMessage).ToList());
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error AddFinancialLaunch");
        return Result.Error(new List<string>() { "Internal Error" });
      }
    }
  }
}
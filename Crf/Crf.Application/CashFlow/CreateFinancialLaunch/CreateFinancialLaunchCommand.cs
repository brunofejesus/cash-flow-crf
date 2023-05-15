using Crf.Application.Common.Models;
using MediatR;

namespace Crf.Application.CashFlow.CreateFinancialLaunch
{
  public class CreateFinancialLaunchCommand : IRequest<Result>
  {
    public string? TypeActivity { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
  }
}
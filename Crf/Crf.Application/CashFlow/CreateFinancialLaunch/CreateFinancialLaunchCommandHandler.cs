using Crf.Application.Common.Interfaces;
using Crf.Application.Common.Models;
using Crf.Application.Interfaces;
using Crf.Domain.Entities;
using Crf.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crf.Application.CashFlow.CreateFinancialLaunch
{
  public class CreateFinancialLaunchCommandHandler : IRequestHandler<CreateFinancialLaunchCommand, Result>
  {
    private readonly ILogger<CreateFinancialLaunchCommandHandler> _logger;
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFinancialLaunchCommandHandler(
      ILogger<CreateFinancialLaunchCommandHandler> logger,
      IAccountRepository repository,
      IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _repository = repository;
      _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateFinancialLaunchCommand request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Running CreateFinancialLaunchCommand...");

      var account = await _repository.GetByFilterAsync(x => x.Name.ToLower().Equals("main"));

      if (account is null)
      {
        return await Task.FromResult(Result.Error(new List<string>()
        {
          "No account was found"
        }));
      }

      var activity = new Activity(
          typeActivity: Enum.Parse<ETypeActivity>(request.TypeActivity!),
          description: request.Description!,
          amount: request.Amount,
          created: request.LaunchDate.HasValue ? request.LaunchDate.Value.ToUniversalTime() : DateTime.UtcNow
        );

      account.AddFinancialActivity(activity);
      _repository.Update(account);
      _unitOfWork.Commit();

      return await Task.FromResult(Result.Success());
    }
  }
}
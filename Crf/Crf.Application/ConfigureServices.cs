using Crf.Application.Common.Behaviours;
using Crf.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;

namespace Crf.Application
{
  public static class ConfigureServices
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      NpgsqlConnection.GlobalTypeMapper.MapEnum<ETypeActivity>("e_type_activity");

      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddMediatR(cfg =>
      {
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
      });

      return services;
    }
  }
}
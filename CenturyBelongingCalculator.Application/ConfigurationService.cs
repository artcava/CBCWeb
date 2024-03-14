using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CenturyBelongingCalculator.Application;

public static class ConfigurationService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}

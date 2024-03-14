using CenturyBelongingCalculator.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CenturyBelongingCalculator.Infrastructure;

public static class ConfigurationService
{
    public static IServiceCollection AddInfrastractureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<ICalcRepository, CalcRepository>();
        return services;
    }
}

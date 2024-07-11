using CarRentalSystem.Web.BackgroundServices;
using CarRentalSystem.Web.Interfaces;

namespace CarRentalSystem.Web.DependencyInjection;

public static class BackgroundServicesServiceCollectionExtension
{
    public static void AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseInitializerService>();

        services.AddSingleton<CountryService>();
        services.AddSingleton<ICountryService, CountryService>();
        services.AddHostedService<CountryService>();
    }
}
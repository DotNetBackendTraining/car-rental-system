using CarRentalSystem.Web.BackgroundServices;

namespace CarRentalSystem.Web.DependencyInjection;

public static class BackgroundServicesServiceCollectionExtension
{
    public static void AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseInitializerService>();
    }
}
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Repositories;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Services;

namespace CarRentalSystem.Web.DependencyInjection;

public static class ApplicationServiceCollection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarService, CarService>();

        services.AddScoped<IAccountService, AccountService>();
        services.Decorate<IAccountService, AccountServiceLoggingDecorator>();

        services.AddScoped<IUserAccessorService, UserAccessorService>();
    }
}
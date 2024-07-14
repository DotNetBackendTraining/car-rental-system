using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Repositories;
using CarRentalSystem.Web.Services;

namespace CarRentalSystem.Web.DependencyInjection;

public static class ApplicationServiceCollection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IAccountService, AccountService>();
    }
}
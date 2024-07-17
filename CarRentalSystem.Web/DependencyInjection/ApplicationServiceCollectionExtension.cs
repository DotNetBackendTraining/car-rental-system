using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Repositories;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Services;
using CarRentalSystem.Web.Settings;

namespace CarRentalSystem.Web.DependencyInjection;

public static class ApplicationServiceCollection
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<ICarService, CarService>();

        services.AddScoped<IUserAccessorService, UserAccessorService>();

        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IEmailConfirmationService, EmailConfirmationService>();
    }
}
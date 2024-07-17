using System.Reflection;
using CarRentalSystem.Core;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarRentalSystem.Web.DependencyInjection;

public static class PresentationServiceCollectionExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AssemblyReference.Assembly);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly);
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddControllersWithViews()
            .AddSessionStateTempDataProvider();

        services.AddRazorPages();
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddCircuitOptions(opt => opt.DetailedErrors = true);

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.SlidingExpiration = true;
        });
    }
}
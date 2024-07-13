using System.Reflection;
using CarRentalSystem.Web.Filters;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Profiles;
using CarRentalSystem.Web.Services;
using CarRentalSystem.Web.ViewModels.Validators;
using FluentValidation;

namespace CarRentalSystem.Web.DependencyInjection;

public static class PresentationServiceCollectionExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationUserProfile)));

        services.AddValidatorsFromAssemblyContaining<RegisterViewModelValidator>();
        services.AddScoped(typeof(ValidationFilter<>));

        services.AddScoped<INotificationService, NotificationService>();

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
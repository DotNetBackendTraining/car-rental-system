using System.Reflection;
using CarRentalSystem.Web.Filters;
using CarRentalSystem.Web.Profiles;
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

        services.AddRazorPages();
        services.AddControllersWithViews();
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
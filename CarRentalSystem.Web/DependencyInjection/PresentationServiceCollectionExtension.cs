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
        services.AddValidatorsFromAssemblyContaining<SignUpViewModelValidator>();
        services.AddScoped(typeof(ValidationFilter<>));
        services.AddRazorPages();
        services.AddControllersWithViews();
    }
}
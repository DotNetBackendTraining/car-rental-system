using CarRentalSystem.Web.Filters;
using CarRentalSystem.Web.ViewModels.Validators;
using FluentValidation;

namespace CarRentalSystem.Web.DependencyInjection;

public static class PresentationServiceCollectionExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SignUpViewModelValidator>();
        services.AddScoped(typeof(ValidationFilter<>));
        services.AddRazorPages();
        services.AddControllersWithViews();
    }
}
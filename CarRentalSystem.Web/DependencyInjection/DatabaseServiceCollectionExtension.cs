using CarRentalSystem.Core;
using CarRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Web.DependencyInjection;

public static class DatabaseServiceCollectionExtension
{
    public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppDbContextConnection") ??
                               throw new ArgumentException("Connection string 'AppDbContextConnection' not found");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = "Default";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddUserValidator<CustomUserValidator<ApplicationUser>>()
            .AddDefaultTokenProviders();
    }
}
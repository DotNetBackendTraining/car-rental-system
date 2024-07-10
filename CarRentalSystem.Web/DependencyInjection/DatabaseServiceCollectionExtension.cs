using CarRentalSystem.Web.Data;
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
    }
}
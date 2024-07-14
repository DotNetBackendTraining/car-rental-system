using CarRentalSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Web.BackgroundServices;

public class DatabaseInitializerService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseInitializerService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);
        await context.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
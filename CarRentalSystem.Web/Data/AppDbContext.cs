using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
using CarRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Core;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.DateOfBirth)
                .IsRequired(false);

            entity.Property(e => e.AddressLine1)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.AddressLine2)
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.DriversLicenseNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
        });

        builder.Entity<Car>(entity =>
        {
            entity.Property(e => e.Make)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Model)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Year)
                .IsRequired();

            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ImageName)
                .HasMaxLength(256);

            entity.HasData(SeedData.GetCarsList());
        });

        builder.Entity<Reservation>(entity =>
        {
            entity.Property(e => e.StartDate)
                .IsRequired();

            entity.Property(e => e.EndDate)
                .IsRequired();

            entity.HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId);

            entity.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
        });
    }
}
using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Core.Interfaces;

public interface IUserAccessorService
{
    Task<ApplicationUser?> GetCurrentUserAsync();
}
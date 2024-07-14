using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Web.Interfaces;

public interface IUserAccessorService
{
    Task<ApplicationUser?> GetCurrentUserAsync();
}
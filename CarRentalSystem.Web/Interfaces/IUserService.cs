using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Web.Interfaces;

public interface IUserService
{
    Task<ApplicationUser?> GetCurrentUserAsync();
}
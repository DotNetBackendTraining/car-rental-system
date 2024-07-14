using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Interfaces;

public interface IUserProfileService
{
    Task<ProfileViewModel> GetCurrentUserProfileAsync();
    Task<IdentityResult> UpdateCurrentUserProfileAsync(ProfileViewModel model);
}
using AutoMapper;
using CarRentalSystem.Web.Models;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<RegisterViewModel, ApplicationUser>();
    }
}
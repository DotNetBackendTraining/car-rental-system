using AutoMapper;
using CarRentalSystem.Core.Entities;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<RegisterViewModel, ApplicationUser>();
    }
}
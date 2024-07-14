using AutoMapper;
using CarRentalSystem.Web.Data.Entities;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<RegisterViewModel, ApplicationUser>();
    }
}
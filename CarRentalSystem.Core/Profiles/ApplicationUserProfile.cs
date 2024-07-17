using AutoMapper;
using CarRentalSystem.Core.ApplicationUsers.Commands.RegisterUser;
using CarRentalSystem.Core.ApplicationUsers.Commands.UpdateUser;
using CarRentalSystem.Core.ApplicationUsers.Queries.CurrentUserQuery;
using CarRentalSystem.Core.Entities;

namespace CarRentalSystem.Core.Profiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<RegisterUserCommand, ApplicationUser>();
        CreateMap<UpdateUserCommand, ApplicationUser>();

        CreateMap<ApplicationUser, CurrentUserResponse>();
    }
}
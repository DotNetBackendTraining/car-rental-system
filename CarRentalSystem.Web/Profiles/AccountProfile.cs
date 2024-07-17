using AutoMapper;
using CarRentalSystem.Core.ApplicationUsers.Commands.ForgotPassword;
using CarRentalSystem.Core.ApplicationUsers.Commands.LoginUser;
using CarRentalSystem.Core.ApplicationUsers.Commands.RegisterUser;
using CarRentalSystem.Core.ApplicationUsers.Commands.ResetPassword;
using CarRentalSystem.Core.ApplicationUsers.Commands.UpdateUser;
using CarRentalSystem.Core.ApplicationUsers.Queries.CurrentUserQuery;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterViewModel, RegisterUserCommand>();
        CreateMap<LoginViewModel, LoginUserCommand>();
        CreateMap<ProfileViewModel, UpdateUserCommand>();
        CreateMap<ForgotPasswordViewModel, ForgotPasswordCommand>();
        CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();

        CreateMap<CurrentUserResponse, ProfileViewModel>();
    }
}
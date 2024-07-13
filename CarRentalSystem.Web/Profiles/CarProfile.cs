using AutoMapper;
using CarRentalSystem.Web.Models;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarProfileViewModel>();
    }
}
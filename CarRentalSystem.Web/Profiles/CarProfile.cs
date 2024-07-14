using AutoMapper;
using CarRentalSystem.Web.Data.Entities;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Profiles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<Car, CarProfileViewModel>();
    }
}
using AutoMapper;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Specification;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Services;

public class CarService : ICarService
{
    private readonly IMapper _mapper;
    private readonly ICarRepository _carRepository;

    public CarService(
        IMapper mapper,
        ICarRepository carRepository)
    {
        _mapper = mapper;
        _carRepository = carRepository;
    }

    // TODO: fix this n+1 queries logic
    public async Task<List<CarProfileViewModel>> GetCarProfilesAsync(CarQuerySpecification specification, DateTime date)
    {
        var cars = await _carRepository.GetCarsAsync(specification);

        var carProfiles = new List<CarProfileViewModel>();
        foreach (var c in cars)
        {
            var cp = _mapper.Map<CarProfileViewModel>(c);
            cp.IsAvailable = await _carRepository.IsCarAvailable(c.Id, date);
            carProfiles.Add(cp);
        }

        return carProfiles;
    }
}
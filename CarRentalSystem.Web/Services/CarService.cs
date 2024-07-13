using AutoMapper;
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
    public async Task<List<CarProfileViewModel>> GetCarProfilesAsync(int page, int pageSize, DateTime date)
    {
        var cars = await _carRepository.GetCarsAsync(page, pageSize);

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
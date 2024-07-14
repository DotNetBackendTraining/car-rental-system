using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Specification;

namespace CarRentalSystem.Core.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarsAsync(CarQuerySpecification specification);

    Task<bool> IsCarAvailable(int carId, DateTime date);
}
using CarRentalSystem.Web.Data.Entities;
using CarRentalSystem.Web.Data.Specification;

namespace CarRentalSystem.Web.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarsAsync(CarQuerySpecification specification);

    Task<bool> IsCarAvailable(int carId, DateTime date);
}
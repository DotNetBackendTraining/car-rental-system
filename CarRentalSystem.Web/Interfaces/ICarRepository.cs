using CarRentalSystem.Web.Data.Entities;

namespace CarRentalSystem.Web.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarsAsync(int page, int pageSize);

    Task<bool> IsCarAvailable(int carId, DateTime date);
}
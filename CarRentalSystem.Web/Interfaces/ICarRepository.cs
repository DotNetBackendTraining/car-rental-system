using CarRentalSystem.Web.Models;

namespace CarRentalSystem.Web.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarsAsync(int page, int pageSize);

    Task<bool> IsCarAvailable(int carId, DateTime date);
}
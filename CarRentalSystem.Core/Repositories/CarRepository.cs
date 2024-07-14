using CarRentalSystem.Core.Entities;
using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Core.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetCarsAsync(CarQuerySpecification specification)
    {
        IQueryable<Car> query = _context.Cars;

        if (!string.IsNullOrEmpty(specification.Location))
        {
            query = query.Where(c => c.Location.Contains(specification.Location));
        }

        return await query
            .OrderBy(c => c.Model)
            .Skip((specification.Page - 1) * specification.PageSize)
            .Take(specification.PageSize)
            .ToListAsync();
    }

    public async Task<bool> IsCarAvailable(int carId, DateTime date)
    {
        var reserved = await _context.Reservations
            .AnyAsync(r => r.CarId == carId && r.StartDate <= date && r.EndDate >= date);
        return !reserved;
    }
}
using CarRentalSystem.Core.Specification;
using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces;

public interface ICarService
{
    Task<List<CarProfileViewModel>> GetCarProfilesAsync(CarQuerySpecification specification, DateTime date);
}
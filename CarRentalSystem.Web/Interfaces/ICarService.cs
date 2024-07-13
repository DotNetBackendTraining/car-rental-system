using CarRentalSystem.Web.ViewModels;

namespace CarRentalSystem.Web.Interfaces;

public interface ICarService
{
    Task<List<CarProfileViewModel>> GetCarProfilesAsync(int page, int pageSize, DateTime date);
}
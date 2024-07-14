using CarRentalSystem.Core.Specification;
using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.Web.Components.Find;

public partial class CarProfiles : ComponentBase
{
    private const int PageSize = 3;

    [Inject] private ICarService CarService { get; set; } = default!;

    private List<CarProfileViewModel> _carProfiles = [];
    private string _searchLocation = string.Empty;
    private DateTime _selectedDate = DateTime.Today;
    private int _currentPage = 1;
    private bool CanNavigateBack => _currentPage > 1;
    private bool _canNavigateForward = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadCarProfiles();
    }

    private async Task LoadCarProfiles()
    {
        var querySpecification = new CarQuerySpecification
        {
            Page = _currentPage,
            PageSize = PageSize,
            Location = _searchLocation
        };
        _carProfiles = await CarService.GetCarProfilesAsync(querySpecification, _selectedDate);
        _canNavigateForward = _carProfiles.Count == PageSize;
    }

    private async Task ChangeSearchLocation(ChangeEventArgs e)
    {
        if (e.Value is not null)
        {
            _searchLocation = e.Value!.ToString();
            await LoadCarProfiles();
        }
    }

    private async Task ChangeSelectedDate(ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out var newDate))
        {
            _selectedDate = newDate;
            _currentPage = 1;
            await LoadCarProfiles();
        }
    }

    private async Task NavigateBack()
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            await LoadCarProfiles();
        }
    }

    private async Task NavigateForward()
    {
        if (_canNavigateForward)
        {
            _currentPage++;
            await LoadCarProfiles();
        }
    }
}
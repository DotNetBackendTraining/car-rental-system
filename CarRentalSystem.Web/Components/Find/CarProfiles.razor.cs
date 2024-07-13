using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.Web.Components.Find;

public partial class CarProfiles : ComponentBase
{
    private const int PageSize = 3;

    [Inject] private ICarService CarService { get; set; } = default!;

    private List<CarProfileViewModel> _carProfiles = [];
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
        _carProfiles = await CarService.GetCarProfilesAsync(_currentPage, PageSize, _selectedDate);
        _canNavigateForward = _carProfiles.Count == PageSize;
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
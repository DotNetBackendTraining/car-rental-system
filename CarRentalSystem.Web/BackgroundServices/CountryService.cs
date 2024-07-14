using CarRentalSystem.Core.Interfaces;
using CarRentalSystem.Web.Interfaces;
using Nager.Country;

namespace CarRentalSystem.Web.BackgroundServices;

/// <summary>
/// Stores all country names and optimizes operations on them.
/// </summary>
public class CountryService : ICountryService, IHostedService
{
    private static readonly HashSet<string> CountriesSet = [];
    private static readonly List<string> CountriesList = [];

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var countryProvider = new CountryProvider();
        var countries = countryProvider.GetCountries()
            .Select(c => c.CommonName)
            .OrderBy(name => name)
            .ToList();

        CountriesList.AddRange(countries);
        foreach (var country in countries)
        {
            CountriesSet.Add(country);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public IEnumerable<string> GetAllCountries()
    {
        return CountriesList;
    }

    public bool IsValidCountry(string country)
    {
        return CountriesSet.Contains(country);
    }
}
namespace CarRentalSystem.Web.Interfaces;

public interface ICountryService
{
    IEnumerable<string> GetAllCountries();

    bool IsValidCountry(string country);
}
using CarRentalSystem.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Web.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string DriversLicenseNumber { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = [];
}
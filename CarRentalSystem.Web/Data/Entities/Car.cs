namespace CarRentalSystem.Web.Data.Entities;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = [];
}
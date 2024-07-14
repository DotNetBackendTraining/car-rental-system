namespace CarRentalSystem.Core.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
namespace CarRentalSystem.Web.ViewModels;

public class CarProfileViewModel
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}
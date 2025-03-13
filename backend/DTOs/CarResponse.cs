public class CarResponse
{
    public int Id { get; set; }
    public required CarType CarType { get; set; }
    public required string FuelType { get; set; }
    public required string ImageUrl { get; set; }
    public required int Mileage { get; set; }
    public required int Seats { get; set; }
    public required string LicensePlate { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public string? Color { get; set; }

}
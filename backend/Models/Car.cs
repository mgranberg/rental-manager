using System.ComponentModel.DataAnnotations;

namespace Models.DB;
public class Car : BaseEntity
{
    public required string Model { get; set; }
    public required string Make { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }

    [MaxLength(7), MinLength(2)]
    public required string LicensePlate { get; set; }
    public int CarTypeId { get; set; }
    public required CarType CarType { get; set; }
    public int Seats { get; set; }

    public int FuelTypeId { get; set; }
    public required FuelType FuelType { get; set; }
    public int Mileage { get; set; }
    public string? ImageUrl { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Models.DB;

public class CarType : BaseEntity
{
    public required string Name { get; set; }

    [Range(typeof(decimal), "1", "100", ErrorMessage = "Multiplier must be greater than 1")]
    public decimal Multiplier { get; set; }
    public bool PayForMileage { get; set; }

    public bool MultiplyDays { get; set; }
    public bool MultiplyMileage { get; set; }
}
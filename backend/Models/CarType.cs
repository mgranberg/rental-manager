using Models.DB;

public class CarType : BaseEntity
{
    public required string Name { get; set; }
    public decimal Multiplier { get; set; }
    public bool PayForMileage { get; set; }

    public bool MultiplyDays { get; set; }
    public bool MultiplyMileage { get; set; }
}
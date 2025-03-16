public class ReturnBookingRequest
{
    public int ReturnMileage { get; set; }
    public required string Identifier { get; set; }
    public bool IsId { get; set; }
    public DateTime? ReturnDate { get; set; }
}
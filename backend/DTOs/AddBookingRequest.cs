namespace backend.DTOs;
public class AddBookingRequest
{
    public required int CarId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string CustomerSsn { get; set; }
}
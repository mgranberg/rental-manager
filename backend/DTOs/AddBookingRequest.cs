using System.ComponentModel.DataAnnotations.Schema;
using Models.DB;

public class AddBookingRequest
{
    public required CarResponse Car { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string CustomerSsn { get; set; }


}
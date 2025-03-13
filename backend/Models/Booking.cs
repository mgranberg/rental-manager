using System.ComponentModel.DataAnnotations.Schema;
using Helpers;

namespace Models.DB;
public class Booking : BaseEntity
{
    public string BookingNumber { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public required int CarId { get; set; }
    public Car Car { get; set; }
    public required int CarTypeId { get; set; }
    public CarType CarType { get; set; }
    public required string EncryptedCustomerSsn { get; set; }

    [NotMapped]
    public string CustomerSsn
    { 
        get => EncryptionHelper.Decrypt(EncryptedCustomerSsn);
        set => EncryptedCustomerSsn = EncryptionHelper.Encrypt(value);
    }
    public required int StartingMileage { get; set; }
    public int EndingMileage { get; set; }
    
    public Booking()
    {
        BookingNumber = GenerateBookingNumber();
    }

    private static string GenerateBookingNumber()
    {
        return $"BNR-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }

}
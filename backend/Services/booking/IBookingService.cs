using Models.DB;

namespace backend.Services;
public interface IBookingService
{
    public Task<IEnumerable<Booking>> GetBookingsAsync();
    public Task<Booking?> GetBookingAsync(int bookingId);
    public Task<Booking> ReturnBookingAsync(Booking booking, int ReturnMileage);
    public Task<Booking> CreateBookingAsync(AddBookingRequest bookingToAdd);
}
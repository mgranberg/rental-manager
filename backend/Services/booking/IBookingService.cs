using backend.DTOs;
using Models.DB;

namespace backend.Services;
public interface IBookingService
{
    public Task<IEnumerable<Booking>> GetBookingsAsync();
    public Task<Booking?> GetBookingAsync(int bookingId);
    public Task<Booking?> GetBookingByBookingNumberAsync(string bookingNumber);
    public Task<Booking?> ReturnBookingAsync(ReturnBookingRequest bookingRequest);
    public Task<Booking> CreateBookingAsync(AddBookingRequest bookingToAdd);
}
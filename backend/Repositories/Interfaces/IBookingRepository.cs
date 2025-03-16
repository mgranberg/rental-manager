using Models.DB;

namespace backend.Repositories.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking> 
{
    Task<Booking?> GetRentedByCarIdAsync(int carId);
    Task<IEnumerable<int>> GetRentedCarIdsAsync();
    Task<IEnumerable<Booking>> GetAllWithCarAndCarTypeAsync();
    Task<Booking?> GetWithCarAndCarTypeIdAsync(int bookingId);
    Task<Booking?> GetWithCarAndCarTypeBookingNoAsync(string bookingNumber);
    
}
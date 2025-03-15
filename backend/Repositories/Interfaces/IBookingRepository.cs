using Models.DB;

namespace backend.Repositories.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking> 
{
    Task<Booking> GetByLicensePlateAsync(string licensePlate);
    Task<Booking> GetRentedByCarIdAsync(int carId);
    Task<IEnumerable<int>> GetRentedCarIdsAsync();
}
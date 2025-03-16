using backend.Data;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Repositories.Implimentations;

public class EfCoreBookingRepository(AppDbContext dbContext) : IBookingRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<Booking> CreateAsync(Booking booking)
    {
        await _dbContext.Bookings.AddAsync(booking);
        await _dbContext.SaveChangesAsync();
        return booking;
    }

    public async Task<IEnumerable<Booking>> CreateRangeAsync(IEnumerable<Booking> bookings)
    {
        await _dbContext.Bookings.AddRangeAsync(bookings);
        await _dbContext.SaveChangesAsync();
        return bookings;
    }

    public async Task<Booking?> DeleteAsync(int id)
    {
        var booking = await _dbContext.Bookings.FindAsync(id);
        if (booking is null) return null;
        _dbContext.Bookings.Remove(booking);
        await _dbContext.SaveChangesAsync();
        return booking;
    }

    public async Task<IEnumerable<Booking>> DeleteRangeAsync(IEnumerable<Booking> bookings)
    {
        _dbContext.Bookings.RemoveRange(bookings);
        await _dbContext.SaveChangesAsync();
        return bookings;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetAllWithCarAndCarTypeAsync()
    {
        return await _dbContext.Bookings
            .Include(b => b.Car)
            .ThenInclude(c => c.CarType)
            .ToListAsync();        
    }

    public async Task<Booking?> GetByIdAsync(int id)
    {
        var booking = await _dbContext.Bookings.FindAsync(id);
        if (booking is null) return null;
        return booking;
    }

    public async Task<Booking?> GetRentedByCarIdAsync(int carId)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.CarId == carId && b.Status == BookingStatus.Rented);
        if (booking is null) return null;
        return booking;
    }

    public async Task<IEnumerable<int>> GetRentedCarIdsAsync()
    {
        return await _dbContext.Bookings
            .Where(b => b.Status == BookingStatus.Rented)
            .Select(b => b.CarId)
            .ToListAsync();
    }

    public async Task<Booking?> GetWithCarAndCarTypeAsync(int bookingId)
    {
        var booking = await _dbContext.Bookings
            .Include(b => b.Car)
            .ThenInclude(c => c.CarType)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
        if (booking is null) return null;
        return booking;        
    }

    public async Task<Booking?> GetWithCarAndCarTypeBookingNoAsync(string bookingNumber)
    {
        var booking = await _dbContext.Bookings
            .Include(b => b.Car)
            .ThenInclude(c => c.CarType)
            .FirstOrDefaultAsync(b => b.BookingNumber == bookingNumber);
        if (booking is null) return null;
        return booking;
    }

    public async Task<Booking?> GetWithCarAndCarTypeIdAsync(int bookingId)
    {
        var booking = await _dbContext.Bookings
            .Include(b => b.Car)
            .ThenInclude(c => c.CarType)
            .FirstOrDefaultAsync(b => b.Id == bookingId);
        if (booking is null) return null;
        return booking;
    }

    public async Task<Booking?> UpdateAsync(Booking booking)
    {
        var bookingToUpdate = await _dbContext.Bookings.FindAsync(booking.Id);
        if (bookingToUpdate is null) return null;
        bookingToUpdate = booking;
        _dbContext.Bookings.Update(bookingToUpdate);
        await _dbContext.SaveChangesAsync();
        return booking;
    }
}
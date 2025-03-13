
using backend.Data;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _dbContext;

    public BookingService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking?> GetBookingAsync(int bookingId)
    {
        return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        return await _dbContext.Bookings.Include(b => b.Car).ToListAsync();
    }

    public async Task<Booking> CreateBookingAsync(AddBookingRequest bookingToAdd)
    {
         var newBooking = new Booking
            {
                CarId = bookingToAdd.Car.Id,
                CarTypeId = bookingToAdd.Car.CarTypeId,
                StartDate = bookingToAdd.StartDate,
                StartingMileage = bookingToAdd.Car.Mileage,
                EndDate = bookingToAdd.EndDate,
                EncryptedCustomerSsn = EncryptionHelper.Encrypt(bookingToAdd.CustomerSsn),
                Status = BookingStatus.Rented,
                ReturnDate = null,
            };
            await _dbContext.Bookings.AddAsync(newBooking);
            await _dbContext.SaveChangesAsync();
            return newBooking;
    }


    public async Task<Booking> ReturnBookingAsync(Booking booking, int ReturnMileage)
    {


        booking.EndDate = DateTime.Now;
        booking.TotalPrice = PriceHelper.CalculatePrice(100, 2, booking.StartDate, booking.EndDate, booking.StartingMileage, booking.EndingMileage, booking.Car.CarType);
        booking.Status = BookingStatus.Returned;
        booking.ReturnDate = DateTime.Now;
        booking.Status = BookingStatus.Returned;
        booking.EndingMileage = ReturnMileage;

        _dbContext.Bookings.Update(booking);
        await _dbContext.SaveChangesAsync();
        
        booking.Car.Mileage = ReturnMileage;
        _dbContext.Cars.Update(booking.Car);
        await _dbContext.SaveChangesAsync();

        return booking;
    }
}

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
                CarTypeId = bookingToAdd.Car.CarType.Id,
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
        var settings = await _dbContext.Settings.FirstOrDefaultAsync();
        if (settings == null)
        {
            throw new Exception("Settings not found");
        }


        booking.EndDate = DateTime.Now;
        booking.Status = BookingStatus.Returned;
        booking.ReturnDate = DateTime.Now;
        booking.Status = BookingStatus.Returned;
        booking.EndingMileage = ReturnMileage;
        booking.TotalPrice = PriceHelper.CalculatePrice(settings.BaseDailyFee, settings.KmFee, booking.StartDate, booking.EndDate, booking.StartingMileage, ReturnMileage, booking.Car.CarType);

        _dbContext.Bookings.Update(booking);
        await _dbContext.SaveChangesAsync();
        
        booking.Car.Mileage = ReturnMileage;
        _dbContext.Cars.Update(booking.Car);
        await _dbContext.SaveChangesAsync();

        return booking;
    }
}
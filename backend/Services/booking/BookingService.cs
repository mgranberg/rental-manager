
using backend.Repositories.Interfaces;
using Helpers;
using Models.DB;

namespace backend.Services;

public class BookingService(IBookingRepository bookingRepository, ISettingRepository settingRepository, ICarRepository carRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private ISettingRepository _settingRepository = settingRepository;
    private ICarRepository _carRepository = carRepository;

    public async Task<Booking?> GetBookingAsync(int bookingId)
    {
        return await _bookingRepository.GetByIdAsync(bookingId);
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync()
    {
        return await _bookingRepository.GetAllAsync();
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
        
        return await _bookingRepository.CreateAsync(newBooking);
    }


    public async Task<Booking> ReturnBookingAsync(Booking booking, int ReturnMileage)
    {
        var settings = (await _settingRepository.GetAllAsync()).FirstOrDefault();
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

        await _bookingRepository.UpdateAsync(booking);
        
        var car = await _carRepository.GetByIdAsync(booking.CarId);
        car.Mileage = ReturnMileage;
        await _carRepository.UpdateAsync(car);

        return booking;
    }
}
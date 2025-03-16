
using backend.DTOs;
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
        var car = await _carRepository.GetByIdWithCartypeWithFueltypeAsync(bookingToAdd.CarId);
        if (car is null)
        {
            throw new Exception("Car not found");
        }
        var newBooking = new Booking
        {
            CarId = car.Id,
            CarTypeId = car.CarType.Id,
            StartDate = bookingToAdd.StartDate,
            StartingMileage = car.Mileage,
            EndDate = bookingToAdd.EndDate,
            EncryptedCustomerSsn = EncryptionHelper.Encrypt(bookingToAdd.CustomerSsn),
            Status = BookingStatus.Rented,
            ReturnDate = null,
        };
        
        return await _bookingRepository.CreateAsync(newBooking);
    }


    public async Task<Booking?> ReturnBookingAsync(ReturnBookingRequest bookingRequest)
    {
        var settings = (await _settingRepository.GetAllAsync()).FirstOrDefault();
        if (settings is null)
        {
            throw new Exception("Settings not found");
        }
        Booking? booking = null;

        if (bookingRequest.IsId && int.TryParse(bookingRequest.Identifier, out var id)) 
        {
            booking = await _bookingRepository.GetWithCarAndCarTypeIdAsync(id);
        } 
        else 
        {
            booking = await _bookingRepository.GetWithCarAndCarTypeBookingNoAsync(bookingRequest.Identifier);
        }

        if (booking is null)
        {
            return null;
        }

        if (booking.Status != BookingStatus.Rented)
        {
            return null;
        }


        booking.EndDate = DateTime.Now;
        booking.Status = BookingStatus.Returned;
        booking.ReturnDate = bookingRequest.ReturnDate ?? DateTime.Now;
        booking.Status = BookingStatus.Returned;
        booking.EndingMileage = bookingRequest.ReturnMileage;
        booking.TotalPrice = PriceHelper.CalculatePrice(settings.BaseDailyFee, settings.KmFee, booking.StartDate, booking.EndDate, booking.StartingMileage, bookingRequest.ReturnMileage, booking.Car.CarType);

        await _bookingRepository.UpdateAsync(booking);
        
        var car = await _carRepository.GetByIdAsync(booking.CarId);
        if (car is null)
        {
            throw new Exception("Car not found");
        }
        car.Mileage = booking.EndingMileage;
        await _carRepository.UpdateAsync(car);

        return booking;
    }
}
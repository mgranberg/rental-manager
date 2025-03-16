using backend.Repositories.Interfaces;

namespace backend.Services;

public class CarService(ICarRepository carRepository, IBookingRepository bookingRepository) : ICarService
{
    private readonly ICarRepository _carRepository = carRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<IEnumerable<CarResponse>> GetAvailableCarsAsync()
    {
        // You could send dates to this and get available cars for the period

        var bookedCarsIds = await _bookingRepository.GetRentedCarIdsAsync();
        var availableCars = await _carRepository.GetAllWithCartypeWithFueltypeAsync();

        var carsToReturn = new List<CarResponse>();

        // Create mapping would be better
        foreach (var car in availableCars)
        {
            // Filter out cars that are already booked
            if (bookedCarsIds.Contains(car.Id)) continue;
            var carResponse = new CarResponse
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                CarType = car.CarType,
                FuelType = car.FuelType.Name,
                ImageUrl = car.ImageUrl ?? "",
                Mileage = car.Mileage,
                Seats = car.Seats,
                LicensePlate = car.LicensePlate,
                Color = car.Color,
                BookingId = null
            };
            carsToReturn.Add(carResponse);
        }   
        return carsToReturn;
    }

    public async Task<IEnumerable<CarResponse>> GetCarsAsync()
    {
        var cars = await _carRepository.GetAllWithCartypeWithFueltypeAsync();
        var carsToReturn = new List<CarResponse>();
        
        // Create mapping would be better
        foreach (var car in cars)
        {
            var carResponse = new CarResponse
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                CarType = car.CarType,
                FuelType = car.FuelType.Name,
                ImageUrl = car.ImageUrl ?? "",
                Mileage = car.Mileage,
                Seats = car.Seats,
                LicensePlate = car.LicensePlate,
                Color = car.Color,
                BookingId = null
            };
            var booking = await _bookingRepository.GetRentedByCarIdAsync(car.Id);
            
            if (booking != null)
            {
                carResponse.BookingId = booking.Id;
            }

            carsToReturn.Add(carResponse);
        }

        return carsToReturn;
    }
}
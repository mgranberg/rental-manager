using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Models.DB;

public class CarsService : ICarsService
{
    private readonly AppDbContext _dbContext;

    public CarsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<CarResponse>> GetCarsAsync()
    {
        var cars = await _dbContext.Cars.Include(c => c.CarType).Include(c => c.FuelType).ToListAsync();
        var carsToReturn = new List<CarResponse>();
        
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
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.CarId == car.Id && b.Status == BookingStatus.Rented);
            if (booking != null)
            {
                carResponse.BookingId = booking.Id;
            }
            carsToReturn.Add(carResponse);
        }

        return carsToReturn;
    }
}
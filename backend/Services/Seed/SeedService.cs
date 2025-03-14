using backend.Data;
using Microsoft.EntityFrameworkCore;
using Models.DB;
namespace backend.Services;
public class SeedService : ISeedService
{
    private readonly AppDbContext _context;

    public SeedService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> SeedAsync()
    {
        await SeedSettingsAsync();
        await SeedCarTypesAsync();
        await SeedFuelTypesAsync();
        await SeedCarsAsync();
        return Results.Created("/seed", "Seeded");
    }

    public async Task<IResult> SeedCarTypesAsync()
    {
        var carTypes = new List<CarType> {
            new CarType { Name = "Småbil", Multiplier = 1.0m, MultiplyDays = false, MultiplyMileage = false, PayForMileage = false },
            new CarType { Name = "Kombi", Multiplier = 1.3m, MultiplyDays = true, MultiplyMileage = false, PayForMileage = true },
            new CarType { Name = "Lastbil", Multiplier = 1.5m, MultiplyDays = true, MultiplyMileage = true, PayForMileage = true }
        };
        await _context.CarTypes.AddRangeAsync(carTypes);
        await _context.SaveChangesAsync();
        return Results.Created("/seed/cartypes", carTypes);
    }

    public async Task<IResult> SeedFuelTypesAsync()
    {
        var fuelTypes = new List<FuelType> {
            new FuelType { Name = "Bensin" },
            new FuelType { Name = "Diesel" },
            new FuelType { Name = "El" }
        };
        await _context.FuelTypes.AddRangeAsync(fuelTypes);
        await _context.SaveChangesAsync();
        return Results.Created("/seed/fueltypes", fuelTypes);
    }

    public async Task<IResult> SeedCarsAsync()
    {
        var carTypes = new List<CarType>();
        var fuelTypes = new List<FuelType>();
        try
        {
            carTypes = await _context.CarTypes.ToListAsync();
            fuelTypes = await _context.FuelTypes.ToListAsync();
            if (carTypes.Count == 0 || fuelTypes.Count == 0)
            {
                return Results.BadRequest("No car types or fuel types found. Please seed car types and fuel types first.");
            }
        }
        catch (Exception)
        {
            return Results.BadRequest("No car types or fuel types found. Please seed car types and fuel types first.");
        }

        var cars = new List<Car> {
            new Car { Make = "Volvo", Model = "V60", Year = 2024, CarType = carTypes[1], LicensePlate = "DEF456", Seats = 5, FuelType = fuelTypes[1], Mileage = 3000, ImageUrl = "" },
            new Car { Make = "Volvo", Model = "V90", Year = 2025, CarType = carTypes[1], LicensePlate = "GHI789", Seats = 7, FuelType = fuelTypes[1], Mileage = 2345, ImageUrl = "" },
            new Car { Make = "Volvo", Model = "EX40", Year = 2025, CarType = carTypes[0], LicensePlate = "JKL012", Seats = 5, FuelType = fuelTypes[2], Mileage = 1221, ImageUrl = "" },
            new Car { Make = "Volkswagen", Model = "Golf", Year = 2024, CarType = carTypes[0], LicensePlate = "MNO345", Seats = 5, FuelType = fuelTypes[0], Mileage = 5000, ImageUrl = "" },
            new Car { Make = "Volkswagen", Model = "Passat", Year = 2025, CarType = carTypes[1], LicensePlate = "PQR678", Seats = 5, FuelType = fuelTypes[0], Mileage = 1000, ImageUrl = "" },
            new Car { Make = "Volkswagen", Model = "Tiguan", Year = 2025, CarType = carTypes[1], LicensePlate = "STU901", Seats = 5, FuelType = fuelTypes[0], Mileage = 2000, ImageUrl = "" },
            new Car { Make = "Volkswagen", Model = "Caddy", Year = 2024, CarType = carTypes[2], LicensePlate = "YZA567", Seats = 3 , FuelType = fuelTypes[1], Mileage = 3000, ImageUrl = "" },
            new Car { Make = "Volkswagen", Model = "Transporter", Year = 2025, CarType = carTypes[2], LicensePlate = "BCD890", Seats = 3, FuelType = fuelTypes[1], Mileage = 4000, ImageUrl = "" }
        };
        await _context.Cars.AddRangeAsync(cars);
        await _context.SaveChangesAsync();
        return Results.Created("/seed/cars", cars);
    }

    public async Task<IResult> SeedSettingsAsync()
    {
        var Setting = new Setting
        {
            BaseDailyFee = 100m,
            KmFee = 1m
        };

        _context.Settings.Add(Setting);
        await _context.SaveChangesAsync();
        return Results.Created("/seed/settings", Setting);
    }
    public async Task<IResult> UnSeedAsync()
    {
        _context.CarTypes.RemoveRange(_context.CarTypes);
        _context.FuelTypes.RemoveRange(_context.FuelTypes);
        _context.Cars.RemoveRange(_context.Cars);
        _context.Settings.RemoveRange(_context.Settings);
        await _context.SaveChangesAsync();
        return Results.Ok("All tables cleared");
    }
}
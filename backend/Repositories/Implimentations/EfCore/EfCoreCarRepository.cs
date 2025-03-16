using backend.Data;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Repositories.Implimentations;

public class EfCoreCarRepository(AppDbContext dbContext) : ICarRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<Car> CreateAsync(Car car)
    {
        await _dbContext.Cars.AddAsync(car);
        await _dbContext.SaveChangesAsync();
        return car;
    }

    public async Task<IEnumerable<Car>> CreateRangeAsync(IEnumerable<Car> cars)
    {
        await _dbContext.Cars.AddRangeAsync(cars);
        await _dbContext.SaveChangesAsync();
        return cars;
    }

    public async Task<Car?> DeleteAsync(int id)
    {
        var car = await _dbContext.Cars.FindAsync(id);
        if (car is null) return null;
        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();
        return car;
    }

    public async Task<IEnumerable<Car>> DeleteRangeAsync(IEnumerable<Car> cars)
    {
        await _dbContext.Cars.AddRangeAsync(cars);
        await _dbContext.SaveChangesAsync();
        return cars;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await _dbContext.Cars.ToListAsync();
    }

    public async Task<IEnumerable<Car>> GetAllWithCartypeWithFueltypeAsync()
    {
        return await _dbContext.Cars
            .Include(c => c.CarType)
            .Include(c => c.FuelType)
            .ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        return await _dbContext.Cars.FindAsync(id);
    }

    public async Task<Car?> GetByIdWithCartypeWithFueltypeAsync(int id)
    {
        return await _dbContext.Cars
            .Include(c => c.CarType)
            .Include(c => c.FuelType)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Car?> GetByLicancePlateWithCarTypeWithFuelTypeAsync(string licensePlate)
    {
        return await _dbContext.Cars
            .Include(c => c.CarType)
            .Include(c => c.FuelType)
            .FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);
    }

    public async Task<Car?> GetByLicensePlateAsync(string licensePlate)
    {
        return await _dbContext.Cars.FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);
    }

    public async Task<Car?> UpdateAsync(Car car)
    {
        var carToUpdate = await _dbContext.Cars.FindAsync(car.Id);
        if (carToUpdate is null) return null;
        _dbContext.Entry(carToUpdate).CurrentValues.SetValues(car);
        await _dbContext.SaveChangesAsync();
        return car;
    }
}
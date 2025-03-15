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

    public async Task<Car> DeleteAsync(int id)
    {
        var car = await _dbContext.Cars.FindAsync(id);
        if (car == null) throw new Exception("Car not found");
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

    public async Task<Car> GetByIdAsync(int id)
    {
        var car = await _dbContext.Cars.FindAsync(id);
        if (car == null) throw new Exception("Car not found");
        return car;
    }

    public Task<Car> GetByLicensePlateAsync(string licensePlate)
    {
        throw new NotImplementedException();
    }

    public async Task<Car> UpdateAsync(Car car)
    {
        var carToUpdate = await _dbContext.Cars.FindAsync(car.Id);
        if (carToUpdate == null) throw new Exception("Car not found");
        carToUpdate = car;
        _dbContext.Cars.Update(carToUpdate);
        await _dbContext.SaveChangesAsync();
        return car;
    }
}
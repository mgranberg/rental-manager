using backend.Data;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Repositories.Implimentations;

public class EfCoreCarTypeRepository(AppDbContext dbContext) : ICarTypeRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<CarType> CreateAsync(CarType carType)
    {
        await _dbContext.CarTypes.AddAsync(carType);
        await _dbContext.SaveChangesAsync();
        return carType;
    }

    public async Task<IEnumerable<CarType>> CreateRangeAsync(IEnumerable<CarType> carTypes)
    {
        await _dbContext.CarTypes.AddRangeAsync(carTypes);
        await _dbContext.SaveChangesAsync();
        return carTypes;
    }

    public async Task<IEnumerable<CarType>> DeleteRangeAsync(IEnumerable<CarType> carTypes)
    {
        _dbContext.CarTypes.RemoveRange(carTypes);
        await _dbContext.SaveChangesAsync();
        return carTypes;
    }

    public async Task<CarType> DeleteAsync(int id)
    {
        var carType = await _dbContext.CarTypes.FindAsync(id);
        if (carType == null) throw new Exception("CarType not found");
        _dbContext.CarTypes.Remove(carType);
        await _dbContext.SaveChangesAsync();
        return carType;
    }

    public async Task<IEnumerable<CarType>> GetAllAsync()
    {
        return await _dbContext.CarTypes.ToListAsync();
    }

    public async Task<CarType> GetByIdAsync(int id)
    {
        var carType = await _dbContext.CarTypes.FindAsync(id);
        if (carType == null) throw new Exception("CarType not found");
        return carType;
    }

    public async Task<CarType> UpdateAsync(CarType carType)
    {
        var carTypeToUpdate = await _dbContext.CarTypes.FindAsync(carType.Id);
        if (carTypeToUpdate == null) throw new Exception("CarType not found");
        carTypeToUpdate = carType;
        _dbContext.CarTypes.Update(carTypeToUpdate);
        await _dbContext.SaveChangesAsync();
        return carTypeToUpdate;
    }
}
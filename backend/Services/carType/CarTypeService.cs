
using backend.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CarTypeService : ICarTypeService
{
    private readonly AppDbContext _dbContext;

    public CarTypeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CarType> AddCarTypeAsync(CarType carType)
    {
        await _dbContext.CarTypes.AddAsync(carType);
        await _dbContext.SaveChangesAsync();
        return carType;
    }

    public async Task<CarType> DeleteCarTypeAsync(int carTypeId)
    {
        await _dbContext.CarTypes.FindAsync(carTypeId);
        var carType = await _dbContext.CarTypes.FindAsync(carTypeId);
        if (carType == null)
        {
            throw new Exception("Car type not found");
        }
        _dbContext.CarTypes.Remove(carType);
        await _dbContext.SaveChangesAsync();
        return carType;
    }

    public async Task<CarType> GetCarTypeAsync(int carTypeId)
    {
        var carType = await _dbContext.CarTypes.FindAsync(carTypeId);

        if (carType == null)
        {
            throw new Exception("Car type not found");
        }
        return carType;
    }

    public async Task<IEnumerable<CarType>> GetCarTypesAsync()
    {
        return await _dbContext.CarTypes.ToListAsync();
    }

    public async Task<CarType> UpdateCarTypeAsync(CarType carType)
    {
        var carTypeToUpdate = await _dbContext.CarTypes.FindAsync(carType.Id);
        if (carTypeToUpdate == null)
        {
            throw new Exception("Car type not found");
        }
        
        _dbContext.Entry(carTypeToUpdate).State = EntityState.Detached;
        carTypeToUpdate = carType;
        _dbContext.CarTypes.Update(carTypeToUpdate);
        await _dbContext.SaveChangesAsync();
        return carTypeToUpdate;
    }
}
using backend.Data;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Repositories.Implimentations
{
    public class EfCoreFuelTypeRepository(AppDbContext dbContext) : IFuelTypeRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<FuelType> CreateAsync(FuelType fuelType)
        {
            await _dbContext.FuelTypes.AddAsync(fuelType);
            await _dbContext.SaveChangesAsync();
            return fuelType;
        }

        public async Task<IEnumerable<FuelType>> CreateRangeAsync(IEnumerable<FuelType> fuelTypes)
        {
            await _dbContext.FuelTypes.AddRangeAsync(fuelTypes);
            await _dbContext.SaveChangesAsync();
            return fuelTypes;
        }

        public async Task<FuelType?> DeleteAsync(int id)
        {
            var fuelType = await _dbContext.FuelTypes.FindAsync(id);
            if (fuelType is null) return null;
            _dbContext.FuelTypes.Remove(fuelType);
            await _dbContext.SaveChangesAsync();
            return fuelType;
        }

        public async Task<IEnumerable<FuelType>> DeleteRangeAsync(IEnumerable<FuelType> fuelTypes)
        {
            _dbContext.FuelTypes.RemoveRange(fuelTypes);
            await _dbContext.SaveChangesAsync();
            return fuelTypes;
        }

        public async Task<IEnumerable<FuelType>> GetAllAsync()
        {
            return await _dbContext.FuelTypes.ToListAsync();
        }

        public async Task<FuelType?> GetByIdAsync(int id)
        {
            var fuelType = await _dbContext.FuelTypes.FindAsync(id);
            if (fuelType is null) return null;
            return fuelType;
        }

        public async Task<FuelType?> UpdateAsync(FuelType fuelType)
        {
            var fuelTypeToUpdate = await _dbContext.FuelTypes.FindAsync(fuelType.Id);
            if (fuelTypeToUpdate is null) return null;
            _dbContext.FuelTypes.Update(fuelType);
            await _dbContext.SaveChangesAsync();
            return fuelType;
        }
    }
}
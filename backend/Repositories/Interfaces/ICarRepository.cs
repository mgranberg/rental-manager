using Microsoft.AspNetCore.Http.HttpResults;
using Models.DB;

namespace backend.Repositories.Interfaces
{

    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<Car?> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Car>> GetAllWithCartypeWithFueltypeAsync();
        Task<Car?> GetByIdWithCartypeWithFueltypeAsync(int id);
        Task<Car?> GetByLicancePlateWithCarTypeWithFuelTypeAsync(string licensePlate);
    }
}
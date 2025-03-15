using Models.DB;

namespace backend.Repositories.Interfaces
{

    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<Car> GetByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Car>> GetAllWithCartypeWithFueltypeAsync();
    }
}
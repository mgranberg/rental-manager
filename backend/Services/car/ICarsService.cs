using Models.DB;

namespace backend.Services;
public interface ICarsService
{
    public Task<IEnumerable<CarResponse>> GetCarsAsync();
}
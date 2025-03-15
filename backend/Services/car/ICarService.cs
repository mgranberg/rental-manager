using Models.DB;

namespace backend.Services;
public interface ICarService
{
    Task<IEnumerable<CarResponse>> GetCarsAsync();
    Task<IEnumerable<CarResponse>> GetAvailableCarsAsync();
}
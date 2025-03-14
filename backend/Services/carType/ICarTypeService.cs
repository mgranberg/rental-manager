namespace backend.Services;
public interface ICarTypeService
{
    Task<IEnumerable<CarType>> GetCarTypesAsync();
    Task<CarType> GetCarTypeAsync(int carTypeId);
    Task<CarType> AddCarTypeAsync(CarType carType);
    Task<CarType> UpdateCarTypeAsync(CarType carType);
    Task<CarType> DeleteCarTypeAsync(int carTypeId);
}
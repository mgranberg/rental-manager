
using backend.Repositories.Interfaces;
using Models.DB;

namespace backend.Services;

public class CarTypeService(ICarTypeRepository carTypeRepository) : ICarTypeService
{
    private readonly ICarTypeRepository _carTypeRepository = carTypeRepository;

    public async Task<CarType> AddCarTypeAsync(CarType carType)
    {
        return await _carTypeRepository.CreateAsync(carType);
    }

    public async Task<CarType> DeleteCarTypeAsync(int carTypeId)
    {
        return await _carTypeRepository.DeleteAsync(carTypeId);
    }

    public async Task<CarType> GetCarTypeAsync(int carTypeId)
    {
        return await _carTypeRepository.GetByIdAsync(carTypeId);
    }

    public async Task<IEnumerable<CarType>> GetCarTypesAsync()
    {
        return await _carTypeRepository.GetAllAsync();
    }

    public async Task<CarType> UpdateCarTypeAsync(CarType carType)
    {
        return await _carTypeRepository.UpdateAsync(carType);
    }
}
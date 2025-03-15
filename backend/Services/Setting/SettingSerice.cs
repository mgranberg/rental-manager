using backend.Repositories.Interfaces;
using Models.DB;

namespace backend.Services;

public class SettingSerice(ISettingRepository settingRepository) : ISettingService
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    public async Task<Setting> GetFirstSettingAsync()
    {
        return await _settingRepository.GetFirstAsync();
    }

    public async Task<Setting> UpdateSettingAsync(Setting setting)
    {
        return await _settingRepository.UpdateAsync(setting);
    }
}
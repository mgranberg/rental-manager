using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DB;

namespace backend.Services;

public class SettingService(ISettingRepository settingRepository) : ISettingService
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    public async Task<Setting?> GetFirstSettingAsync()
    {
        return await _settingRepository.GetFirstAsync();
    }

    public async Task<Setting?> UpdateSettingAsync(Setting setting)
    {
        return await _settingRepository.UpdateAsync(setting);
    }
}
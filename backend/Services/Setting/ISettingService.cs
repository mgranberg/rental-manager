using Models.DB;

namespace backend.Services;

public interface ISettingService
{
    Task<Setting> GetFirstSettingAsync();
    Task<Setting> UpdateSettingAsync(Setting setting);
    
}
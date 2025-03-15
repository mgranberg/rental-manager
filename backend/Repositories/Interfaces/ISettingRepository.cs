using Models.DB;

namespace backend.Repositories.Interfaces;
public interface ISettingRepository : IGenericRepository<Setting>
{
    Task<Setting> GetFirstAsync();
}
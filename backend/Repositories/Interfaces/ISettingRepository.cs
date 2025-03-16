using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DB;

namespace backend.Repositories.Interfaces;
public interface ISettingRepository : IGenericRepository<Setting>
{
    Task<Setting?> GetFirstAsync();
}
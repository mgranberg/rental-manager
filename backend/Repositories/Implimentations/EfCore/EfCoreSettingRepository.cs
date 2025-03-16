using backend.Data;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Repositories.Implimentations;

public class EfCoreSettingRepository(AppDbContext dbContext) : ISettingRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<Setting> CreateAsync(Setting setting)
    {
        _dbContext.Settings.Add(setting);
        await _dbContext.SaveChangesAsync();
        return setting;
    }

    public async Task<IEnumerable<Setting>> CreateRangeAsync(IEnumerable<Setting> settings)
    {
        await _dbContext.Settings.AddRangeAsync(settings);
        await _dbContext.SaveChangesAsync();
        return settings;
    }

    public Task<Setting?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Setting>> DeleteRangeAsync(IEnumerable<Setting> settings)
    {
        _dbContext.Settings.RemoveRange(settings);
        await _dbContext.SaveChangesAsync();
        return settings;
    }

    public async Task<IEnumerable<Setting>> GetAllAsync()
    {
        return await _dbContext.Settings.ToListAsync();
    }

    public async Task<Setting?> GetByIdAsync(int id)
    {
        return await _dbContext.Settings.FindAsync(id);
    }

    public async Task<Setting?> GetFirstAsync()
    {
        return await _dbContext.Settings.FirstOrDefaultAsync();
    }

    public async Task<Setting?> UpdateAsync(Setting setting)
    {
        var settingToUpdate = await _dbContext.Settings.FindAsync(setting.Id);
        
        if (settingToUpdate is null) return null;
        
        _dbContext.Entry(settingToUpdate).CurrentValues.SetValues(setting);
        await _dbContext.SaveChangesAsync();
        return settingToUpdate;
    }
}
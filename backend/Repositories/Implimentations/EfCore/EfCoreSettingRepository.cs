using backend.Data;
using backend.Repositories.Interfaces;
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

    public async Task<Setting> DeleteAsync(int id)
    {
        var setting = await _dbContext.Settings.FindAsync(id);
        if (setting == null) throw new Exception("Setting not found");
        _dbContext.Settings.Remove(setting);
        await _dbContext.SaveChangesAsync();
        return setting;
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

    public async Task<Setting> GetByIdAsync(int id)
    {
        var res = await _dbContext.Settings.FindAsync(id);
        if (res == null) throw new Exception("Setting not found");
        return res;
    }

    public async Task<Setting> GetFirstAsync()
    {
        var setting = await _dbContext.Settings.FirstOrDefaultAsync();
        if (setting == null) throw new Exception("Setting not found");
        return setting;
    }

    public async Task<Setting> UpdateAsync(Setting setting)
    {
        var settingToUpdate = await _dbContext.Settings.FindAsync(setting.Id);
        if (settingToUpdate == null) throw new Exception("Setting not found");
        settingToUpdate = setting;
        _dbContext.Settings.Update(settingToUpdate);
        await _dbContext.SaveChangesAsync();
        return settingToUpdate;
    }
}
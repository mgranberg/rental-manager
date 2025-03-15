using backend.Data;
using backend.Repositories.Interfaces;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;

public static class SettingsEndpoint
{
    public static void MapSettingsEndpoints(this WebApplication app)
    {
        app.MapGet("/settings", async (ISettingService settingService) =>
        {
            return await settingService.GetFirstSettingAsync();
        });

        app.MapPut("/settings", async (ISettingRepository settingRepository, Setting setting) =>
        {
            var settingToUpdate = await settingRepository.GetByIdAsync(setting.Id);
            if (settingToUpdate == null)
            {
                return Results.NotFound();
            }

            settingToUpdate = setting;
            await settingRepository.UpdateAsync(settingToUpdate);
            return Results.Ok();
        });
    }
}
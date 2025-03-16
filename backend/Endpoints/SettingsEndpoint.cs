using backend.Services;
using Models.DB;

namespace backend.Endpoints;

public static class SettingsEndpoint
{
    public static void MapSettingsEndpoints(this WebApplication app)
    {
        app.MapGet("/settings", async (ISettingService settingService) =>
        {
            var setting =  await settingService.GetFirstSettingAsync();
            if (setting is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(setting);
        });

        app.MapPut("/settings", async (ISettingService settingService, Setting setting) =>
        {
            var settingToUpdate = await settingService.UpdateSettingAsync(setting);
            if (settingToUpdate is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(settingToUpdate);
        });
    }
}
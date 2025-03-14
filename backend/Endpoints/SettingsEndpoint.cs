using backend.Data;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;

public static class SettingsEndpoint
{
    public static void MapSettingsEndpoints(this WebApplication app)
    {
        app.MapGet("/settings", async (AppDbContext context) =>
        {
            return await context.Settings.FirstOrDefaultAsync();
        });

        app.MapPut("/settings", async (AppDbContext context, Setting setting) =>
        {
            var settingToUpdate = await context.Settings.FindAsync(setting.Id);
            if (settingToUpdate == null)
            {
                return Results.NotFound();
            }
             // Detach the existing entity
            context.Entry(settingToUpdate).State = EntityState.Detached;

            settingToUpdate = setting;
            context.Settings.Update(settingToUpdate);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}
using backend.Services;

public static class SeedEndpoints
{
    public static void MapSeedEndpoints(this WebApplication app)
    {
        app.MapPost("/seed", async (ISeedService seedService) =>
        {
            return await seedService.SeedAsync();
        });

        app.MapPost("/seed/cartypes", async (ISeedService seedService) =>
        {
            return await seedService.SeedCarTypesAsync();
        });

        app.MapPost("/seed/fueltypes", async (ISeedService seedService) =>
        {
          return await seedService.SeedFuelTypesAsync();
        });

        app.MapPost("/seed/cars", async (ISeedService seedService) =>
        {
          return await seedService.SeedCarsAsync();
        });
    }
}
using backend.Services;

namespace backend.Endpoints;
public static class CarsEndpoints
{
    public static void MapCarsEndpoints(this WebApplication app)
    {
        app.MapGet("/cars", async (ICarService carService) =>
        {
            return await carService.GetCarsAsync();
        });

        app.MapGet("/cars/availableCars", async (ICarService carService) =>
        {
            return await carService.GetAvailableCarsAsync();
        });
    }
}
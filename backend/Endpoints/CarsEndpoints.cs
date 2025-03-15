using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;
public static class CarsEndpoints
{
    public static void MapCarsEndpoints(this WebApplication app)
    {
        app.MapGet("/cars", async (ICarService carService) =>
        {
            return await carService.GetCarsAsync();
        });

        app.MapPost("/cars/availableCars", async (ICarService carService) =>
        {
            return await carService.GetAvailableCarsAsync();
        });

    }
}
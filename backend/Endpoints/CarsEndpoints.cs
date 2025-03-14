using backend.Data;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;
public static class CarsEndpoints
{
    public static void MapCarsEndpoints(this WebApplication app)
    {
        app.MapGet("/cars", async (HttpContext context) =>
        {
            var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
            var CarsService = context.RequestServices.GetRequiredService<ICarsService>();
            return await CarsService.GetCarsAsync();
        });

        app.MapPost("/cars/availableCars", async (AppDbContext dbContext, AvailableCarsRequest availableCarsRequest) =>
        {

            var bookedCarsIds = await dbContext.Bookings
                .Where(b => b.Status == BookingStatus.Rented)
                .Select(b => b.CarId)
                .ToListAsync();

            // Filter out cars that are already booked
            var availableCars = await dbContext.Cars
                .Include(c => c.CarType)
                .Include(c => c.FuelType)
                .Where(c => !bookedCarsIds.Contains(c.Id))
                .ToListAsync();

            return Results.Ok(availableCars);
        });

    }
}
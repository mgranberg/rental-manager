using backend.Data;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;
public static class CarsEndpoints
{
    public static void MapCarsEndpoints(this WebApplication app)
    {
        app.MapGet("/cars", async (AppDbContext dbContext) =>
        {
            return await dbContext.Cars.Include(c => c.CarType).Include(c => c.FuelType).ToListAsync();
        });

        app.MapPost("/cars/availableCars", async (AppDbContext dbContext, AvailableCarsRequest availableCarsRequest) =>
        {
            // TODO make this return cars that are available between the start and end date
            var availableCars = await dbContext.Cars
                .Include(c => c.CarType)
                .Include(c => c.FuelType)
                .ToListAsync();
            Random random = new Random();
            int randomCount = random.Next(1, availableCars.Count + 1); // Pick a random number of elements

            return availableCars.OrderBy(x => Guid.NewGuid()) // Shuffle the list
                                .Take(randomCount) // Take a random number of elements
                                .ToList();
        });

        app.MapGet("/cars/{id}", async (AppDbContext dbContext, int id) =>
        {
            return await dbContext.Cars.FindAsync(id);
        });

        app.MapPost("/cars", async (AppDbContext dbContext, Car car) =>
        {
            await dbContext.Cars.AddAsync(car);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/cars/{car.Id}", car);
        });

        app.MapPut("/cars/{id}", async (AppDbContext dbContext, int id, Car car) =>
        {
            if (id != car.Id)
            {
                return Results.BadRequest("Id mismatch");
            }

            dbContext.Cars.Update(car);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/cars/{id}", async (AppDbContext dbContext, int id) =>
        {
            var car = await dbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return Results.NotFound();
            }

            dbContext.Cars.Remove(car);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
using backend.Services;
using Models.DB;

namespace backend.Endpoints;

public static class CarTypesEndpoints
{
    public static void MapCarTypesEndpoints(this WebApplication app)
    {
        app.MapGet("/cartypes", async (ICarTypeService carTypeService) =>
        {
            return await carTypeService.GetCarTypesAsync();
        });
        app.MapGet("/cartypes/{carTypeId:int}", async (ICarTypeService carTypeService, int carTypeId) =>
        {
            var carType = await carTypeService.GetCarTypeAsync(carTypeId);
            if (carType is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(carType);
        });

        app.MapPost("/cartypes", async (ICarTypeService carTypeService, CarType carType) =>
        {
            if (carType.Multiplier < 1)
            {
                return Results.BadRequest("Multiplier must be greater than 1");
            }

            if (carType.Name is null)
            {
                return Results.BadRequest("Name must not be null");
            }

            return Results.Created("/CarTypes", await carTypeService.AddCarTypeAsync(carType));
        });

        app.MapPut("/cartypes", async (ICarTypeService carTypeService, CarType carType) =>
        {
            var res =  await carTypeService.UpdateCarTypeAsync(carType);
            if (res is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(res);
        });

        app.MapDelete("/cartypes/{carTypeId:int}", async (ICarTypeService carTypeService, int carTypeId) =>
        {
            var res = await carTypeService.DeleteCarTypeAsync(carTypeId);
            if (res is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(res);
        });
    }
}
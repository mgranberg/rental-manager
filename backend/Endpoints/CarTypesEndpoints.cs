using backend.Services;

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
            return await carTypeService.GetCarTypeAsync(carTypeId);
        });

        app.MapPost("/cartypes", async (ICarTypeService carTypeService, CarType carType) =>
        {
            return await carTypeService.AddCarTypeAsync(carType);
        });

        app.MapPut("/cartypes", async (ICarTypeService carTypeService, CarType carType) =>
        {
            return await carTypeService.UpdateCarTypeAsync(carType);
        });

        app.MapDelete("/cartypes/{carTypeId:int}", async (ICarTypeService carTypeService, int carTypeId) =>
        {
            return await carTypeService.DeleteCarTypeAsync(carTypeId);
        });
    }
}
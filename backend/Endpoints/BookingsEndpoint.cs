using backend.Data;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DB;

namespace backend.Endpoints;
public static class BookingsEndpoint
{
    public static void MapBookingsEndpoint(this WebApplication app)
    {
        app.MapGet("/bookings", async (HttpContext context) =>
        {
            var bookingService = context.RequestServices.GetRequiredService<IBookingService>();
            return await bookingService.GetBookingsAsync();
        });

        app.MapGet("/bookings/{id}", async (HttpContext context) =>
        {
            var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
            var bookingService = context.RequestServices.GetRequiredService<IBookingService>();
            
            var idValue = context.Request.RouteValues["id"] as string;
            if (idValue == null || !int.TryParse(idValue, out var id))
            {
                return Results.BadRequest("Invalid booking ID");
            }

            var booking = await bookingService.GetBookingAsync(id);

            if (booking == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(booking);
        });

        app.MapPost("/bookings", async (HttpContext context) =>
        {
            var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
            var bookingService = context.RequestServices.GetRequiredService<IBookingService>();
            var booking = await context.Request.ReadFromJsonAsync<AddBookingRequest>();

            if (booking == null)
            {
                return Results.BadRequest("Invalid request body");
            }

            var result = await bookingService.CreateBookingAsync(booking);
            return Results.Created($"/bookings/{result.Id}", result);
        });

        app.MapPut("/bookings/return/{identifier}", async (HttpContext context) =>
        {
            var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
            var bookingService = context.RequestServices.GetRequiredService<IBookingService>();
            var identifier = context.Request.RouteValues["identifier"] as string;
            var bookingRequest = await context.Request.ReadFromJsonAsync<ReturnBookingRequest>();

            if (bookingRequest == null)
            {
                return Results.BadRequest("Invalid request body");
            }

            Booking? bookingToReturn = null;
            if (int.TryParse(identifier, out var id))
            {
                bookingToReturn = await dbContext.Bookings.Include(b => b.Car).FirstOrDefaultAsync(b => b.Id == id);
            }
            else
            {
                bookingToReturn = await dbContext.Bookings.Include(b => b.Car).FirstOrDefaultAsync(b => b.BookingNumber == identifier);
            }

            if (bookingToReturn == null)
            {
                return Results.NotFound();
            }

            if (bookingToReturn.Status != BookingStatus.Rented)
            {
                return Results.BadRequest("Booking is not currently rented");
            }

            if (bookingToReturn.Car.Mileage > bookingRequest.ReturnMileage)
            {
                return Results.BadRequest($"Ending mileage cannot be less than starting mileage of {bookingToReturn.Car.Mileage} km");
            }

            var result = await bookingService.ReturnBookingAsync(bookingToReturn, bookingRequest.ReturnMileage);
            return Results.Ok(result);
        });
    }
}
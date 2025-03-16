using backend.DTOs;
using backend.helpers;
using backend.Services;

namespace backend.Endpoints;
public static class BookingsEndpoint
{
    public static void MapBookingsEndpoint(this WebApplication app)
    {
        app.MapGet("/bookings", async (IBookingService bookingService) =>
        {
            return await bookingService.GetBookingsAsync();
        });

        app.MapGet("/bookings/{id}", async (IBookingService bookingService, int id) =>
        {
            var booking = await bookingService.GetBookingAsync(id);

            if (booking is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(booking);
        });

        app.MapGet("/bookings/bookingNumber/{bookingNumber}", async (IBookingService bookingService, string bookingNumber) =>
        {
            var booking = await bookingService.GetBookingByBookingNumberAsync(bookingNumber);

            if (booking is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(booking);
        });

        app.MapPost("/bookings", async (IBookingService bookingService, AddBookingRequest bookingRequest) =>
        {
            if (bookingRequest is null)
            {
                return Results.BadRequest("Invalid request body");
            }

            if (!Validator.IsValidSsn(bookingRequest.CustomerSsn))
            {
                return Results.BadRequest("Invalid SSN");
            }

            var result = await bookingService.CreateBookingAsync(bookingRequest);
            return Results.Created($"/bookings/{result.Id}", result);
        });

        app.MapPut("/bookings/return", async (IBookingService bookingService, ReturnBookingRequest returnBookingRequest) =>
        {

            if (returnBookingRequest is null)
            {
                return Results.BadRequest("Invalid request body");
            }

            var result = await bookingService.ReturnBookingAsync(returnBookingRequest);

            if (result is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(result);
        });
    }
}
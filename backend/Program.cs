using backend.Data;
using backend.Endpoints;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// configure sqlite db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=rental-manager.db");
});

// services
builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<ICarTypeService, CarTypeService>();

var app = builder.Build();

app.MapSeedEndpoints();
app.MapCarsEndpoints();
app.MapBookingsEndpoint();
app.MapCarTypesEndpoints();
app.MapSettingsEndpoints();

app.Run();

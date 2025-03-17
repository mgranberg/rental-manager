using backend.Data;
using backend.Endpoints;
using backend.Repositories.Implimentations;
using backend.Repositories.Interfaces;
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


var repo = "efCore";

// Chose repository

switch (repo)
{
    case "efCore":
        builder.Services.AddScoped<ICarTypeRepository, EfCoreCarTypeRepository>();
        builder.Services.AddScoped<ICarRepository, EfCoreCarRepository>();
        builder.Services.AddScoped<ISettingRepository, EfCoreSettingRepository>();
        builder.Services.AddScoped<IFuelTypeRepository, EfCoreFuelTypeRepository>();
        builder.Services.AddScoped<IBookingRepository, EfCoreBookingRepository>();
        
        break;

    case "other":
        // builder.Services.AddScoped<ICarTypeRepository, OtherCarTypeRepository>();
        // builder.Services.AddScoped<ICarRepository, OtherCarRepository>();
        // builder.Services.AddScoped<IBookingRepository, OtherBookingRepository>();
        break;
}

// services
builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarTypeService, CarTypeService>();
builder.Services.AddScoped<ISettingService, SettingService>();

var app = builder.Build();

app.MapSeedEndpoints();
app.MapCarsEndpoints();
app.MapBookingsEndpoint();
app.MapCarTypesEndpoints();
app.MapSettingsEndpoints();

app.Run();

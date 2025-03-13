using Microsoft.EntityFrameworkCore;

namespace backend.Data;
using Models.DB;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Car> Cars { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<FuelType> FuelTypes { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}
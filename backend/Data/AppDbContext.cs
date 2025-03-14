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
    public DbSet<Setting> Settings { get; set; }

    public override int SaveChanges()
    {
        UpdateTimes();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateTimes();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateTimes() 
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity trackable)
            {
                var now = DateTime.Now;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedAt = now;
                        break;
                    case EntityState.Added:
                        trackable.CreatedAt = now;
                        trackable.UpdatedAt = now;
                        break;
                }
            }
        }
    }
}
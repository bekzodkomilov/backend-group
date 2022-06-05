using Microsoft.EntityFrameworkCore;
using TaxiDrivers.Entities;

namespace TaxiDrivers.Data;
public class AppDbContext : DbContext
{
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}
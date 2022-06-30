using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.ApiEntities;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Context;
public class ApiDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        :base(options) { }
}
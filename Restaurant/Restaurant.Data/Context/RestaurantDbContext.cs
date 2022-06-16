using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.ApiEntities;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Context;
public class RestaurantDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookedDish> BookedDishes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        :base(options) { }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql();
    // }
}
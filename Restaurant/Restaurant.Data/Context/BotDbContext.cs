using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.ApiEntities;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Context;
public class BotDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<BookedDish> BookedDishes { get; set; }
    public DbSet<User> Users { get; set; }
    public BotDbContext(DbContextOptions<BotDbContext> options)
        :base(options) { }
}
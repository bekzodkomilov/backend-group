using Microsoft.EntityFrameworkCore;

namespace SQLite.Entities;
public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) {} 
}
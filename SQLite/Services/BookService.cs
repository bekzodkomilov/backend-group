using SQLite.Entities;

namespace SQLite.Services;
public class BookService : IBookService
{
    private readonly ILogger<BookService> _logger;
    private readonly BookDbContext _context;

    public BookService(ILogger<BookService> logger, BookDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task AddAsync(Book newBook)
    {
        await _context.SaveChangesAsync();
        await _context.Books.AddAsync(newBook);
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await GetByIdAsync(id);
        await _context.SaveChangesAsync();
        _context.Books.Remove(book);
    }

    public Task<List<Book>> GetAllAsync()
    {
        var books = _context.Books.ToList<Book>();
        return null;
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Update(book);
        await _context.SaveChangesAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly ILogger<BooksRepository> _logger;
    private readonly BotDbContext _context;

    public BooksRepository(ILogger<BooksRepository> logger, BotDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id)
    {
        try
        {
            var b = await GetByIdAsync(id);
            _context.Books.Remove(b);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Book with {b.Id} id was deleted");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Book was not deleted.\nError: {e.Message}");
            return (false, e);
        }
    }

    public async Task<List<Book>> GetAllAsync(Func<Book, bool> p = null)
    {
        return _context.Books.Include(b => b.User).Include(b => b.BookedDishes).Where(p).ToList();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await _context.Books.Include(p => p.User).Include(p => p.BookedDishes).FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Books.AnyAsync(p => p.Id == id);
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Book with {book.Id} id was added");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Book was not added.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(Book book)
    {
        try
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Book with {book.Id} was updated.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Book was not updated.\nMessage: {e.Message}");
            return (false, e);
        }
    }
}
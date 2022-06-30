using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories;
public class BookedDishesRepository : IBookedDishesRepository
{
    private readonly BotDbContext _context;
    private readonly ILogger<BookedDishesRepository> _logger;

    public BookedDishesRepository(BotDbContext context, ILogger<BookedDishesRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id)
    {
        try
        {
            var bd = await GetByIdAsync(id);
            _context.BookedDishes.Remove(bd);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Booked dish with {bd.Id} was deleted.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Booked dish wasn't deleted.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<List<BookedDish>> GetAllAsync(Func<BookedDish, bool> p = null)
    {
        return _context.BookedDishes.Include(p => p.Book).Where(p).ToList();
    }

    public async Task<List<BookedDish>> GetByBookIdAsync(Guid id)
    {
        var bds = _context.BookedDishes.Include(p => p.Book).Where(p => p.BookId == id).ToList();
        return bds;
    }

    public async Task<BookedDish> GetByIdAsync(Guid id)
    {
        return await _context.BookedDishes.Include(p => p.Book).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(BookedDish bookedDish)
    {
        try
        {
            await _context.BookedDishes.AddAsync(bookedDish);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Booked dish with {bookedDish.Id} id was added");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Booked dish was not added.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(BookedDish bookedDish)
    {
        try
        {
            _context.BookedDishes.Update(bookedDish);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Booked dish with {bookedDish.Id} was updated.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError("Booked dish was not updated.");
            return (false, e);
        }
    }
}
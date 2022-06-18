using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories;
public class BookedDishesRepository : IBookedDishesRepository
{
    private readonly RestaurantDbContext _context;
    private readonly ILogger<BookedDishesRepository> _logger;

    public BookedDishesRepository(RestaurantDbContext context, ILogger<BookedDishesRepository> logger)
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
        return _context.BookedDishes.Where(p).ToList();
    }

    public async Task<List<BookedDish>> GetByBookIdAsync(Guid id)
    {
        var bds = _context.BookedDishes.Where(p => p.BookId == id).ToList();
        return bds;
    }

    public async Task<BookedDish> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, Exception e)> InsertAsync(BookedDish bookedDish)
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, Exception e)> UpdateAsync(BookedDish bookedDish)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.ApiEntities;

namespace Restaurant.Data.Repositories;
public class DishesRepository : IDishesRepository
{
     private readonly ApiDbContext _context;
    private readonly ILogger<DishesRepository> _logger;

    public DishesRepository(ApiDbContext context, ILogger<DishesRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id)
    {
        try
        {
            var d = await GetByIdAsync(id);
            _context.Dishes.Remove(d);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Dish with {d.Id} was deleted.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Dish wasn't deleted.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Dishes.AnyAsync(d => d.Id == id);
    }

    public async Task<List<Dish>> GetAllAsync(Func<Dish, bool> d = null)
    {
        if(d is null)
            d = new Func<Dish, bool>(p => true);
        return _context.Dishes.Include(d => d.Category).Where(d).ToList();
    }

    public async Task<List<Dish>> GetByCategoryIdAsync(Guid id)
     {
        var ds = _context.Dishes.Include(d => d.Category).Where(d => d.CategoryId == id).ToList();
        return ds;
    }

    public async Task<Dish> GetByIdAsync(Guid id)
    {
        return await _context.Dishes.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(Dish dish)
    {
        try
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Dish with {dish.Id} id was added");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Dish was not added.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(Dish dish)
    {
        try
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Dish with {dish.Id} was updated.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError("Dish was not updated.");
            return (false, e);
        }
    }

}

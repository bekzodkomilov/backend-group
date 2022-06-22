using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.ApiEntities;

namespace Restaurant.Data.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly RestaurantDbContext _context;
    private readonly ILogger<CategoriesRepository> _logger;

    public CategoriesRepository(RestaurantDbContext context, ILogger<CategoriesRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id)
    {
        try
        {
            var cd = await GetByIdAsync(id);
            _context.Categories.Remove(cd);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Category dish with {cd.Id} was deleted.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Category dish wasn't deleted.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<List<Category>> GetAllAsync(Func<Category, bool> p = null)
    {
        return _context.Categories.Include(p => p.Dishes).Where(p).ToList();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        var cate = _context.Categories.Include(p => p.Dishes).FirstOrDefault(p => p.Id == id);
        return cate;
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(Category category)
    {
         try
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Category dish with {category.Id} id was added");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Category dish was not added.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(Category category)
    {
        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Category dish with {category.Id} was updated.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError("Category dish was not updated.");
            return (false, e);
        }
    }
}
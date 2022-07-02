using Restaurant.Domain.Entities.ApiEntities;

namespace Restaurant.Data.Repositories.Interfaces;
public interface IDishesRepository
{
    Task<Dish> GetByIdAsync(Guid id);
    Task<List<Dish>> GetAllAsync(Func<Dish, bool> d = null);
    Task<List<Dish>> GetByCategoryIdAsync(Guid id);
    Task<(bool IsSuccess, Exception e)> InsertAsync(Dish dish);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(Dish dish);
    Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
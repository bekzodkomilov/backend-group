using Restaurant.Domain.Entities.ApiEntities;

namespace Restaurant.Data.Repositories.Interfaces;

public interface ICategoriesRepository
{
    Task<Category> GetByIdAsync(Guid id);
    Task<List<Category>> GetAllAsync(Func<Category, bool> p = null);
    Task<(bool IsSuccess, Exception e)> InsertAsync(Category category);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(Category category);
    Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id);

}
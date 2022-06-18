using System.Linq.Expressions;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories.Interfaces;
public interface IBookedDishesRepository
{
    Task<BookedDish> GetByIdAsync(Guid id);
    Task<List<BookedDish>> GetAllAsync(Func<BookedDish, bool> p = null);
    Task<List<BookedDish>> GetByBookIdAsync(Guid id);
    Task<(bool IsSuccess, Exception e)> InsertAsync(BookedDish bookedDish);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(BookedDish bookedDish);
    Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id);
}
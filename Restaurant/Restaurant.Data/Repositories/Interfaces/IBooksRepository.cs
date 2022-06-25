using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories.Interfaces;

public interface IBooksRepository
{
    Task<Book> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync(Func<Book, bool> p = null);
    Task<(bool IsSuccess, Exception e)> InsertAsync(Book book);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(Book book);
    Task<(bool IsSuccess, Exception e)> DeleteIdAsync(Guid id);
}
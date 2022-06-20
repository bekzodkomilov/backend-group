using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories.Interfaces;

public interface IBooksRepository
{
    Task<(bool IsSucces, Exception e)> GetAllAsync(Func<Book, bool> p = null);
    Task<(bool IsSucces, Exception e)> GetByIdAsync(Guid id);
    Task<(bool IsSucces, Exception e)> InsertAsync(Book book);
    Task<(bool IsSucces, Exception e)> UpdateAsync(Book book);
    Task<(bool IsSucces, Exception e)> DeleteIdAsync(Guid id);
}
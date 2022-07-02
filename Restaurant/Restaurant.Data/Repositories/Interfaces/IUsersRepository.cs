using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories.Interfaces;
public interface IUsersRepository
{
    Task<User> GetByIdAsync(long id);
    Task<List<User>> GetAllAsync(Func<User, bool> p = null);
    Task<(bool IsSuccess, Exception e)> InsertAsync(User user);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(User user);
    Task<(bool IsSuccess, Exception e)> DeleteIdAsync(long id);
    Task<bool> ExistsAsync(long id);  
}

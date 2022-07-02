using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data.Context;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Domain.Entities.BotEntities;

namespace Restaurant.Data.Repositories;
public class UsersRepository : IUsersRepository
{
    private readonly BotDbContext _context;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(BotDbContext context, ILogger<UsersRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, Exception e)> DeleteIdAsync(long id)
    {
        try
        {
            var u = await GetByIdAsync(id);
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"User with {u.ChatId} chatId was deleted");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"User was not deleted.\nError: {e.Message}");
            return (false, e);
        }
    }
    public async Task<bool> ExistsAsync(long id)
    {
        return await _context.Users.AnyAsync(d => d.ChatId == id);
    }

    public async Task<List<User>> GetAllAsync(Func<User, bool> p = null)
    {
        return _context.Users.Include(p => p.Books).Where(p).ToList();
    }

    public async Task<User> GetByIdAsync(long id)
    {
        return await _context.Users.Include(p => p.Books).FirstOrDefaultAsync(p => p.ChatId == id);
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"User with {user.ChatId} chatId was added");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"User was not added.\nMessage: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"User with {user.ChatId} was updated.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"User was not updated.\nError: {e.Message}");
            return (false, e);
        }
    }
}
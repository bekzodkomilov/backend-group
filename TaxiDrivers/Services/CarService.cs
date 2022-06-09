using Microsoft.EntityFrameworkCore;
using TaxiDrivers.Data;
using TaxiDrivers.Entities;

namespace TaxiDrivers.Services;
public class CarService : IEntityService<Car>
{
    private readonly ILogger<CarService> _logger;
    private readonly AppDbContext _context;

    public CarService(ILogger<CarService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<(bool IsSuccess, Exception e)> InsertAsync(Car entity)
    {
        try
        {
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New car is added to database with {entity.Id}");
            return (true, null);
        }
        catch(Exception e)
        {
            _logger.LogError($"New car was not added. Exception:\n{e.Message}");
            return (false, e);
        }
    }

    public Task<(bool IsSuccess, Exception e)> UpdateAsync(Car entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Car> GetByIdAsync(Guid id)
    {
        try
        {
            var car = _context.Cars.Include(c => c.Driver).FirstOrDefault(d => d.Id == id);
            return car;
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public Task<List<Car>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, Exception e)> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
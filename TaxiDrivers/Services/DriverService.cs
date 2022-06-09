using Microsoft.EntityFrameworkCore;
using TaxiDrivers.Data;
using TaxiDrivers.Entities;

namespace TaxiDrivers.Services;
public class DriverService : IEntityService<Driver>
{
    private readonly ILogger<DriverService> _logger;
    private readonly AppDbContext _context;

    public DriverService(ILogger<DriverService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public Task<(bool IsSuccess, Exception e)> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Driver>> GetAllAsync()
    {
        return _context.Drivers.Include(d => d.Car).Where(d => true).ToList();
    }

    public async Task<Driver> GetByIdAsync(Guid id)
    {
        try
        {
            var driver = _context.Drivers.Include(d => d.Car).FirstOrDefault(d => d.Id == id);
            return driver;
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<(bool IsSuccess, Exception e)> InsertAsync(Driver entity)
    {
        try
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.CarId == entity.CarId);
            if(driver != default)
            {
                throw new Exception("Bu mashinani olib bo'lmaydi!");
            }
            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New driver is added to database with {entity.Id}");
            return (true, null);
        }
        catch(Exception e)
        {
            _logger.LogError($"New driver was not added. Exception:\n{e.Message}");
            return (false, e);
        }
    }

    public Task<(bool IsSuccess, Exception e)> UpdateAsync(Driver entity)
    {
        throw new NotImplementedException();
    }
}
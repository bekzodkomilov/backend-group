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
    public async Task<(bool IsSuccess, Exception e, Car entity)> InsertAsync(Car entity)
    {
        try
        {
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New car is added to database with {entity.Id}");
            return (true, null, entity);
        }
        catch(Exception e)
        {
            _logger.LogError($"New car was not added. Exception:\n{e.Message}");
            return (false, e, null);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> UpdateAsync(Car entity)
    {
        try
        {
            _context.Cars.Update(entity);
            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Car was not updated.\n{e.Message}");
            return (false, e);
        }
    }

    public async Task<Car> GetByIdAsync(Guid id)
    {
        try
        {
            var car = _context.Cars.Include(c => c.Drivers).FirstOrDefault(d => d.Id == id);
            return car;
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return _context.Cars.Include(c => c.Drivers).ToList();
    }

    public async Task<List<Car>> GetByDriverIdAsync(Guid id)
    {
        return _context.Cars.Include(c => c.Drivers).ToList();
    }

    public async Task<(bool IsSuccess, Exception e)> DeleteAsync(Guid id)
    {
        try
        {
            var car = await GetByIdAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception e)
        {
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception e)> AddDriverAsync(Guid id, Guid driverId)
    {
        try
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
            if(driver == default) throw new Exception("Driver doesn't exist");
            var car = await GetByIdAsync(id);
            car.Drivers.Add(driver);
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Ok");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong:\n{e.Message}");
            return (false, e);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using TaxiDrivers.Entities;
using TaxiDrivers.Models;
using TaxiDrivers.Services;

namespace TaxiDrivers.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly ILogger<DriverController> _logger;
    private readonly DriverService _service;

    public DriverController(ILogger<DriverController> logger, DriverService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("/adddriver")]
    public async Task<IActionResult> AddDriver([FromForm]NewDriver newDriver)
    {
        var driver = new Driver(){
            Id = Guid.NewGuid(),
            FirstName = newDriver.FirstName,
            LastName = newDriver.LastName,
            Age = newDriver.Age,
            PhoneNumber = newDriver.PhoneNumber,
            CarId = newDriver.CarId
        };
        var result = await _service.InsertAsync(driver);
        var error = !result.IsSuccess;
        var message = result.e is null ? "Success" : result.e.Message;
        return Ok(new {error, message});
    }

    [HttpGet("/getdriver")]
    public async Task<IActionResult> GetDriver([FromQuery]Guid id)
    {
        var driver = await _service.GetByIdAsync(id);
        return Ok(driver);
    } 

    [HttpGet("/getall")]
    public async Task<IActionResult> GetAllDriver()
    {
        var drivers = await _service.GetAllAsync();
        return Ok(drivers);
    }

    [HttpPut("updatedriver/{driverId}")]
    public async Task<IActionResult> UpdareDriver([FromForm]UpdateDriver updateDriver, Guid driverId)
    {
        var driver = await _service.GetByIdAsync(driverId);
        driver.CarId = updateDriver.CarId ?? driver.CarId;
        driver.PhoneNumber = updateDriver.PhoneNumber ?? driver.PhoneNumber;
        var result = await _service.UpdateAsync(driver);
        var error = !result.IsSuccess;
        var message = result.e is null ? "Success" : result.e.Message;
        return Ok(new {error, message});
    }
}
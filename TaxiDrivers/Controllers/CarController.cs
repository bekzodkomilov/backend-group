using Microsoft.AspNetCore.Mvc;
using TaxiDrivers.Entities;
using TaxiDrivers.Models;
using TaxiDrivers.Services;

namespace TaxiDrivers.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ILogger<CarController> _logger;
    private readonly CarService _service;

    public CarController(ILogger<CarController> logger, CarService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost("/addcar")]
    public async Task<IActionResult> AddCar([FromForm]NewCar newCar)
    {
        var car = new Car(){
            Id = Guid.NewGuid(),
            Model = newCar.Model,
            Number = newCar.Number,
            Color = newCar.Color,
            Type = newCar.Type,
            DriverId = newCar.DriverId
        };
        var result = await _service.InsertAsync(car);
        var error = !result.IsSuccess;
        var message = result.e is null ? "Success" : result.e.Message;
        return Ok(new {error, message, car});
    }

    [HttpGet("/getcar/{id}")]
    public async Task<IActionResult> GetCar(Guid id)
    {
        var car = await _service.GetByIdAsync(id);
        return Ok(car);
    }

    [HttpGet("/getallcars")]
    public async Task<IActionResult> GetAllCar()
    {
        var cars = await _service.GetAllAsync();
        return Ok(cars);
    }

    [HttpPut("/updatecar/{carId}")]
    public async Task<IActionResult> UpdateCar([FromForm]UpdateCar updateCar, Guid carId)
    {
        var car = await _service.GetByIdAsync(carId);
        car.Color = updateCar.Color ?? car.Color;
        car.DriverId = updateCar.DriverId ?? car.DriverId;
        car.Number = updateCar.Number ?? car.Number;
        var result = await _service.UpdateAsync(car);
        var error = !result.IsSuccess;
        var message = result.e is null ? "Success" : result.e.Message;
        return Ok(new {error, message});
    }
}
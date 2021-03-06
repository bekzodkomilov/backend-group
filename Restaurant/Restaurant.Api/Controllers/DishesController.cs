using Microsoft.AspNetCore.Mvc;
using Restaurant.Service.Services;
using Restaurant.ViewModel.DishViewModels;

namespace Restaurant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DishesController : ControllerBase
{
    private readonly DishService _service;
    private readonly ILogger<DishesController> _logger;

    public DishesController(DishService service, ILogger<DishesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("/adddish")]
    public async Task<IActionResult> AddDish([FromForm]CreateDishViewModel model)
    {
        var dish = await _service.CreateDishAsync(model);
        if(dish) return Ok(dish);
        return BadRequest(dish);
    }

    [HttpGet("getdishesbycategoryid/{id}")]
    public async Task<IActionResult> GetDishesByCategoryId(Guid id)
    {
        var res = await _service.GetAllDishByCategoryIdAsync(id);
        return Ok(res);
    }

    [HttpGet("getalldishes")]
    public async Task<IActionResult> GetAllDishes()
    {
        var res = await _service.GetAllDishesAsync();
        return Ok(res);
    }

    [HttpDelete("deletedish/{id}")]
    public async Task<IActionResult> DeleteDishById(Guid id)
    {
        _logger.LogInformation("controller was called");
        var res = await _service.DeleteByIdAsync(id);
        return Ok(res);
    }

    [HttpPut("updatedish/{id}")]
    public async Task<IActionResult> UpdateDish([FromForm]UpdateDishViewModel dish, Guid id)
    {
        var res = await _service.UpdateDishAsync(dish, id);
        return Ok(res);
    }

    [HttpGet("image/{id}")]
    public async Task<IActionResult> GetDishImage(Guid id)
    {
        var image = await _service.GetImageAsync(id);
        byte[] data = System.Convert.FromBase64String(image.Replace("data:image/jpeg;base64,/", "/"));
        MemoryStream ms = new MemoryStream(data);
        return Ok(ms);
    }
    [HttpPut("getdishbyid/{id}")]
    public async Task<IActionResult> GetDishById(Guid id)
    {
        var result = await _service.GetDishById(id);
        return Ok(result);
    }
}
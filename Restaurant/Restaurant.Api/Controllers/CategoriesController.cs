using Microsoft.AspNetCore.Mvc;
using Restaurant.Service.Services;
using Restaurant.ViewModel.CategoryViewModels;

namespace Restaurant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _service;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(CategoryService service, ILogger<CategoriesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("addcategory")]
    public async Task<IActionResult> AddCategory([FromForm]CreateCategoryViewModel model)
    {
        var response = await _service.CreateCategoryAsync(model);
        if(response) return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllCategory()
    {
        var result = await _service.GetAllCategoriesAsync();
        return Ok(result);
    }
}
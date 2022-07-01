using Microsoft.Extensions.Logging;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Service.Mappers;
using Restaurant.ViewModel.DishViewModels;

namespace Restaurant.Service.Services;
public class DishService
{
    private readonly IDishesRepository _repo;
    private readonly ILogger<DishService> _logger;

    public DishService(IDishesRepository repo, ILogger<DishService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<bool> CreateDishAsync(CreateDishViewModel newDish)
    {
        var result = await _repo.InsertAsync(newDish.ToEntity());
        return result.IsSuccess;
    }
    public async Task<List<GetDishViewModel>> GetAllDishesAsync()
    {
        var result = (await _repo.GetAllAsync()).Select(d => d.ToModel()).ToList();
        return result;
    }

    public async Task<List<GetDishViewModel>> GetAllDishByCategoryIdAsync(Guid CategoryId)
    {
        var result = (await _repo.GetByCategoryIdAsync(CategoryId)).Select(d => d.ToModel()).ToList();
        return result;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        _logger.LogInformation("Method was called");
        var result = await _repo.DeleteIdAsync(id);
        return result.IsSuccess;
    }
    
    public async Task<bool> UpdateDishAsync(UpdateDishViewModel model, Guid id)
    {
        var dish = await _repo.GetByIdAsync(id);
        dish.Price = model.Price;
        var result = await _repo.UpdateAsync(dish);
        return result.IsSuccess;
    }

    public async Task<string> GetImageAsync(Guid id)
    {
        var dish = await _repo.GetByIdAsync(id);
        return dish.Image;
    }
    public async Task<GetDishViewModel> GetDishById(Guid id)
    {
        var res = await _repo.GetByIdAsync(id);
        return res.ToModel();
    }
}
using Microsoft.Extensions.Logging;
using Restaurant.Data.Repositories;
using Restaurant.Service.Mappers;
using Restaurant.ViewModel.DishViewModels;

namespace Restaurant.Service.Services;
public class DishService
{
    private readonly DishesRepository _repo;
    private readonly ILogger<DishService> _logger;

    public DishService(DishesRepository repo, ILogger<DishService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<bool> CreateDishAsync(CreateDishViewModel newDish)
    {
        var result = await _repo.InsertAsync(newDish.ToEntity());
        return result.IsSuccess;
    }

    public async Task<List<GetDishViewModel>> GetAllDishByCategoryIdAsync(Guid CategoryId)
    {
        var result = (await _repo.GetByCategoryIdAsync(CategoryId)).Select(d => d.ToModel()).ToList();
        return result;
    }
    
}
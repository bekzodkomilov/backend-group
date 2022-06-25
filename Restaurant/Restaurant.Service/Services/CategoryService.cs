using Microsoft.Extensions.Logging;
using Restaurant.Data.Repositories.Interfaces;
using Restaurant.Service.Mappers;
using Restaurant.ViewModel.CategoryViewModels;

namespace Restaurant.Service.Services;

public class CategoryService
{
    private readonly ICategoriesRepository _repo;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoriesRepository repo, ILogger<CategoryService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<bool> CreateCategoryAsync(CreateCategoryViewModel newCategory)
    {
        var result = await _repo.InsertAsync(newCategory.ToEntity());
        return result.IsSuccess;
    }
    public async Task<List<GetCategoryViewModel>> GetAllCategoriesAsync()
    {
        var result = (await _repo.GetAllAsync()).Select(d => d.ToModel()).ToList();
        return result;
    }
}
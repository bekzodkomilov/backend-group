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

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var result = await _repo.DeleteIdAsync(id);
        return result.IsSuccess;
    }

    public async Task<bool> UpdateCategoryAsync(UpdateCategoryViewModel model, Guid id)
    {
        var cate = await _repo.GetByIdAsync(id);
        cate.Name = model.Name;
        var result = await _repo.UpdateAsync(cate);
        return result.IsSuccess;
    }

    public async Task<GetCategoryViewModel> GetCategoryByIdAsync(Guid id)
    {
        var res = await _repo.GetByIdAsync(id);
        return res.ToModel();
    }
}
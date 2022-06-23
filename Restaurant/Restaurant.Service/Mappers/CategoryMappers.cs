using Restaurant.Domain.Entities.ApiEntities;
using Restaurant.ViewModel.CategoryViewModels;

namespace Restaurant.Service.Mappers;

public static class CategoryMappers
{
    public static Category ToEntity(this CreateCategoryViewModel model)
        => new Category()
        {
            Id = Guid.NewGuid(),
            Name = model.Name
        };

    public static GetCategoryViewModel ToModel(this Category category)
        => new GetCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name
        };
}
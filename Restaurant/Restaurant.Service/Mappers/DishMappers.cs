using System;
using Restaurant.Domain.Entities.ApiEntities;
using Restaurant.ViewModel.DishViewModels;

namespace Restaurant.Service.Mappers;
public static class DishMappers
{
    public static Dish ToEntity(this CreateDishViewModel model)
        => new Dish()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Price = model.Price,
            CategoryId = model.CategoryId
        };

    public static GetDishViewModel ToModel(this Dish dish)
        => new GetDishViewModel()
        {
            Id = dish.Id,
            Name = dish.Name,
            Price = dish.Price,
            CategoryId = dish.CategoryId,
            Category = dish.Category.Name
        };
}
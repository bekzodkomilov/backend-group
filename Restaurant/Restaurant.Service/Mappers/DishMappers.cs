using Microsoft.AspNetCore.Http;
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
            CategoryId = model.CategoryId,
            Image = model.Image.toByte()
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

    public static string toByte(this IFormFile image)
    {
        var memoryStream = new MemoryStream();
        image.CopyToAsync(memoryStream);
        var result = memoryStream.ToArray();
        while(result.Count() == 0) result = memoryStream.ToArray();
        var str = Convert.ToBase64String(result);
        return "data:image/jpeg;base64,"+str;
    }
}
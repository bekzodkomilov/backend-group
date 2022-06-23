using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.DishViewModels;

public class GetDishViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public string Category { get; set; }
    public Guid CategoryId { get; set; }
}
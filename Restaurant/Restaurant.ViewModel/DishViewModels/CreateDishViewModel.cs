using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.DishViewModels;

public class CreateDishViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public long Price { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
}
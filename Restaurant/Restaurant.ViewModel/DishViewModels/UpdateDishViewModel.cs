using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.DishViewModels;

public class UpdateDishViewModel
{
    [Required]
    public long Price { get; set; }
}
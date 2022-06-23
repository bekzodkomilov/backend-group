using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.BookedDishViewModels;
public class CreateBookedDishViewModel
{
    [Required]
    public Guid DishId { get; set; }
    
    [Required]
    public Guid BookId { get; set; }
    
    [Required]
    public int Amount { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.DishViewModels;

public class CreateBookViewModel
{
    [Required]
    public long UserChatId { get; set; }  
}
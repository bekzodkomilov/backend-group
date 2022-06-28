using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Restaurant.ViewModel.DishViewModels;

public class CreateDishViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public long Price { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }

    [Display(Name="File")]
    public IFormFile Image { get; set; }
}
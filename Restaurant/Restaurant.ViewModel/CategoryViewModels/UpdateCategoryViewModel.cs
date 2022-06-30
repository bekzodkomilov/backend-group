using System.ComponentModel.DataAnnotations;
namespace Restaurant.ViewModel.CategoryViewModels;

public class UpdateCategoryViewModel
{
    [Required]
    public string Name { get; set; }
}
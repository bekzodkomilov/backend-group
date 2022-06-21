using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModel.CategoryViewModels;
public class CreateCategoryViewModel
{
    [Required]
    public string Name { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities.ApiEntities;
public class Category
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Dish> Dishes { get; set; }
}
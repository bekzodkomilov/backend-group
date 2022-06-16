using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.Domain.Entities.ApiEntities;
public class Dish
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
}
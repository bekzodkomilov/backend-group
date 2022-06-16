using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities.BotEntities;
public class BookedDish
{
    [Key]
    public Guid Id { get; set; }
    public Guid DishId { get; set; }
    public string DishName { get; set; }
    public Guid BookId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities.BotEntities;
public class Book
{
    [Key]
    public Guid Id { get; set; }
    public long UserChatId { get; set; }
    public DateTime BookedTime { get; set; }
    public long Price { get; set; }
    public ICollection<BookedDish> BookedDishes { get; set; }
    
    [ForeignKey(nameof(UserChatId))]
    public User User { get; set; }
}
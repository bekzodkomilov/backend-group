using System.ComponentModel.DataAnnotations;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities.BotEntities;
public class User
{
    [Key]
    public long ChatId { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string PhoneNumber { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Address { get; set; }
    public EProcess Process { get; set; }
    public ICollection<Book> Books { get; set; }
}
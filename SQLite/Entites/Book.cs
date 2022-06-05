using System.ComponentModel.DataAnnotations;

namespace SQLite.Entities;
public class Book
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }
    
    [MaxLength(256)]
    public string Title { get; set; }

}
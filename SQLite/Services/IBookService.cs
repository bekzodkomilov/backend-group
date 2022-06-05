using SQLite.Entities;

namespace SQLite.Services;
public interface IBookService
{
    Task<Book> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
    Task DeleteAsync(Guid id);
    Task AddAsync(Book newBook);
    Task UpdateAsync(Book book);
}
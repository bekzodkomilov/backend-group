namespace TaxiDrivers.Services;
public interface IEntityService<TEntity> where TEntity : class
{
    Task<(bool IsSuccess, Exception e)> InsertAsync(TEntity entity);
    Task<(bool IsSuccess, Exception e)> UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task<(bool IsSuccess, Exception e)> DeleteAsync(Guid id);
}
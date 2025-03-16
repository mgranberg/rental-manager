namespace backend.Repositories.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);
    Task<T?> UpdateAsync(T entity);
    Task<T?> DeleteAsync(int id);
    Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities);
}
namespace LockNote.Data;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id, string partitionKey);
    Task<IEnumerable<T>> GetAllAsync(string query);
    Task AddAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}
namespace Server.Repositories.Interfaces
{
    internal interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> DeleteAsync(long id);
        Task<T> GetAsync(long id);
        Task<int> AddRangeAsync(IEnumerable<T> list);
        Task ReplaceAsync(T t);
        Task<long> AddAsync(T t);
    }
}

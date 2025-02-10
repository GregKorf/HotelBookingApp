namespace HotelBookingApp.Repositories
{
    public interface IBaseRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        

    }
}

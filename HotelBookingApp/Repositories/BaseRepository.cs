
using HotelBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly HotelBookingDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(HotelBookingDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public virtual Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existingEntity = await GetByIdAsync(id);

            if(existingEntity is null) return false;

            dbSet.Remove(existingEntity);

            return true;
        }

        public virtual async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);


        public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

    }
}

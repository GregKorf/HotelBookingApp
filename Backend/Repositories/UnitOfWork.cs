
using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelBookingDbContext _context;

        public UnitOfWork(HotelBookingDbContext context)
        {
            _context = context;
        }

        public UserRepository UserRepository => new(_context);

        public AdminRepository AdminRepository => new(_context);

        public CustomerRepository CustomerRepository => new(_context);

        public BookingRepository BookingRepository => new(_context);

        public RoomRepository RoomRepository => new(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

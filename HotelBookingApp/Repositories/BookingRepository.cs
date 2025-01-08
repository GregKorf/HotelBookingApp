using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<List<Booking>> GetAllActiveBookingsAsync()
        {
            return await context.Bookings
                .Where(b => b.Status == BookingStatus.Active)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            return await context.Bookings
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }
        
        public async Task<List<Booking>> GetBookingsByRoomIdAsync(int id)
        {
            return await context.Bookings
                .Where (b => b.RoomId == id)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByDateRangeAsync(DateTime checkInDate, DateTime checkOutDate)
        {
            return await context.Bookings
                .Where(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
                .ToListAsync();
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return !await context.Bookings
                .AnyAsync(b => b.RoomId == roomId
                           && b.Status == BookingStatus.Active
                           && b.CheckInDate < checkOutDate
                           && b.CheckOutDate > checkInDate);
        }
    }
}

using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {

        public RoomRepository(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<Room?> GetRoomByNumberAsync(string? roomNumber)
        {
            return await context.Rooms
                .Where(r => r.RoomNumber == roomNumber)
                .SingleOrDefaultAsync();
        }
        public async Task<List<Room>> GetRoomsByTypeAsync(RoomType roomType)
        {
            return await context.Rooms
                .Where(r => r.RoomType == roomType)
                .ToListAsync();
        }

        public async Task<decimal?> GetRoomPriceAsync(int roomId)
        {
            return await context.Rooms
                .Where(r => r.Id == roomId)
                .Select(r => r.RoomPricePerNight)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId)
        {
           return await context.Rooms
                .Where(r => r.Id == roomId)
                .SelectMany(r => r.Bookings)
                .ToListAsync();
        }
        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await context.Rooms
                 .Where(r => r.Id == id)
                 .SingleOrDefaultAsync();
        }
    }
}

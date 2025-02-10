using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public interface IRoomRepository
    {
        Task<Room?> GetRoomByNumberAsync(string? roomNumber);
        Task<List<Room>> GetRoomsByTypeAsync(RoomType roomType);
        Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId);
        Task<decimal?> GetRoomPriceAsync(int roomId);
        Task<Room?> GetRoomByIdAsync(int id);
    }
}

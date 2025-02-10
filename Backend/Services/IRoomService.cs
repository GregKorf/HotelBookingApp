using HotelBookingApp.DTO;

namespace HotelBookingApp.Services
{
    public interface IRoomService
    {
        Task<List<RoomReadOnlyDTO>> GetAllRoomsAsync();
        Task<RoomReadOnlyDTO> GetRoomByIdAsync(int roomId);
        Task<RoomReadOnlyDTO> CreateRoomAsync(RoomCreateDTO roomDTO);
        Task<bool> UpdateRoomPriceAsync(int Id, decimal newPrice);
    }
}

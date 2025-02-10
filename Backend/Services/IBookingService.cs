using HotelBookingApp.DTO;

namespace HotelBookingApp.Services
{
    public interface IBookingService
    {
        Task<List<BookingReadOnlyDTO>> GetBookingsActiveAsync();
        Task<List<BookingReadOnlyDTO>> GetBookingsByCustomerIdAsync(int customerId);
        Task<List<BookingReadOnlyDTO>> GetBookingsByRoomIdAsync(int roomId);
        Task<BookingReadOnlyDTO> CancelBookingAsync(int bookingId);
    }
}

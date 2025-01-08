using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllActiveBookingsAsync();
        Task<List<Booking>> GetBookingsByRoomIdAsync(int id);
        Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId);
        //ToConsider CustomerId with specific BookingId
        Task<List<Booking>> GetBookingsByDateRangeAsync(DateTime checkInDate, DateTime checkOutDate);  


        Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate);


    }
}

using HotelBookingApp.Data;
using HotelBookingApp.DTO;

namespace HotelBookingApp.Services
{
    public interface ICustomerService
    {
        Task<UserReadOnlyDTO> SignUpCustomerAsync(CustomerSignUpDTO request);
        Task<List<BookingReadOnlyDTO>> GetCustomerBookingsAsync(int id);
        Task<UserCustomerReadOnlyDTO?> GetCustomerByUsernameAsync(string username);
        Task<List<UserCustomerReadOnlyDTO>> GetAllCustomersAsync();
        Task<BookingReadOnlyDTO> BookRoomAsync(BookingCreateDTO bookingRequest, int customerId);
        Task<Customer?> GetCustomerByUserIdAsync(int userId);

    }
}

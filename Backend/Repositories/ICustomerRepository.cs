using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByPhoneNumberAsync(string phoneNumber);
        Task<List<User>> GetAllUsersCustomersAsync();
        Task<User?> GetUserCustomerByUsernameAsync(string userName);
        Task<Customer?> GetCustomerByUserIdAsync(int userId);
    }
}

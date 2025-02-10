using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
        Task<User?> GetUserByUsername(string username);

       
    }
}

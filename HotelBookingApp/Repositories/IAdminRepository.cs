using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin?> GetByJobDescriptionAsync(string jobDescription);
        Task<Admin?> GetAdminByPhoneNumberAsync(string phoneNumber);
        Task<List<User>> GetAllUsersAdminsAsync();
        Task<User?> GetUserAdminByUsernameAsync(string userName);
    }
}

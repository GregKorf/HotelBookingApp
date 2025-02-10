using HotelBookingApp.Data;
using HotelBookingApp.DTO;

namespace HotelBookingApp.Services
{
    public interface IAdminService
    {
        Task<UserReadOnlyDTO> SignUpAdminAsync(AdminSignUpDTO request);
        Task<List<UserAdminReadOnlyDTO>> GetAllUsersAdminsAsync();
        Task<UserAdminReadOnlyDTO?> GetAdminByUsernameAsync(string username);
    }
}

using HotelBookingApp.Core.Filters;
using HotelBookingApp.Data;

namespace HotelBookingApp.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize,
            UserFiltersDTO userFiltersDTO);

        Task<User?> GetUserByIdAsync(int id);
    }
}

using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(HotelBookingDbContext context) : base(context)
        {
        }


        public async Task<Admin?> GetAdminByPhoneNumberAsync(string? phoneNumber)
        {
            return await context.Admins
                .Where(a => a.PhoneNumber == phoneNumber)
                .SingleOrDefaultAsync();
        }

        public async Task<Admin?> GetByJobDescriptionAsync(string? jobDescription)
        {
            return await context.Admins
            .Where(a => a.JobDescription == jobDescription)
            .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAdminsAsync()
        {
            var usersWithAdminRole = await context.Users
                .Where(u => u.UserRole == UserRole.Admin)
                .Include(u => u.Admin)
                .ToListAsync();

            return usersWithAdminRole;
        }



        public async Task<User?> GetUserAdminByUsernameAsync(string userName)
        {
            var userAdmin = await context.Users
                .Include(u => u.Admin)  // This is crucial - it loads the Admin navigation property
                .SingleOrDefaultAsync(u => u.Username == userName);


            return userAdmin;
        }
    }
}

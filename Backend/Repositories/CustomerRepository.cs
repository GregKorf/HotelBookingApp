using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(HotelBookingDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerByPhoneNumberAsync(string? phoneNumber)
        {
            return await context.Customers
                .Where(c => c.PhoneNumber == phoneNumber)
                .SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersCustomersAsync()
        {
            var usersWithCustomerRole = await context.Users
                 .Where(u => u.UserRole == UserRole.Customer)
                 .Include(u => u.Customer)
                 .ToListAsync();

            return usersWithCustomerRole;
        }



        public async Task<User?> GetUserCustomerByUsernameAsync(string userName)
        {
            var userCustomer = await context.Users
                .Where(u => u.UserRole == UserRole.Customer && u.Username == userName)
                .SingleOrDefaultAsync();

            return userCustomer;
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await context.Customers
                .Where(c => c.UserId == userId)
                .SingleOrDefaultAsync();
        }
    }
}

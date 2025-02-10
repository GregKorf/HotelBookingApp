using HotelBookingApp.Data;
using HotelBookingApp.Encryption;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HotelBookingDbContext context) : base(context)
        {
        }


        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username
            || x.Email == username);
            if (user == null)
            {
                return null;
            }
            if (!EncryptionUtil.VerifyPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }



        public async Task<User?> GetUserByUsername(string username)
        {
            return await context.Users
                .Where(u => u.Username == username)
                .SingleOrDefaultAsync();
        }
    }
}

using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.Data
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public UserRole UserRole { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Customer? Customer { get; set; }

    }
}

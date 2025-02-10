using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class UserAdminReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserRole? UserRole { get; set; }
        public string? JobDescription { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

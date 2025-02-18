﻿using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class UserCustomerReadOnlyDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserRole? UserRole { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }

        public List<BookingReadOnlyDTO>? Bookings { get; set; }
    }
}

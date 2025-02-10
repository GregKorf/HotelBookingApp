﻿using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelBookingApp.DTO
{
    public class AdminSignUpDTO
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [RegularExpression(@"(?=.*?[A-Z])(?=.*?[a-z])(?=.*?\d)(?=.*?\W)^.{8,}$",
            ErrorMessage = "Password must contain at least one uppercase, one lowercase, " +
            "one digit, and one special character")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(?:\s+[A-Za-z]+)*$", ErrorMessage = "Firstname can only contain letters and spaces.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z]+(?:\s+[A-Za-z]+)*$", ErrorMessage = "Lastname can only contain letters and spaces.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be at least 10 characters and not exceed 15 characters.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job Description must be between 2 and 50 characters.")]
        public string? JobDescription { get; set; }

    }
}

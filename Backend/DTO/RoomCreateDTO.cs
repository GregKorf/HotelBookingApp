using System.ComponentModel.DataAnnotations;
using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class RoomCreateDTO
    {
        [Required]
        [Range(10.00, 9999.99, ErrorMessage = "Price must be between $10 and $9,999.99")]
        public decimal RoomPricePerNight { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(4, MinimumLength = 2, ErrorMessage = "Room Number must be between 2 and 4 characters.")]
        public string? RoomNumber { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [EnumDataType(typeof(RoomType), ErrorMessage = "Invalid RoomType")]
        public RoomType RoomType { get; set; }
    }
}

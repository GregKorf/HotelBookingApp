using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class BookingUpdateDTO
    {
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [EnumDataType(typeof(BookingStatus), ErrorMessage = "Invalid Booking Status")]
        public BookingStatus Status { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.DTO
{
    public class BookingCreateDTO
    {
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }



        [Required]
        public int RoomId { get; set; } 
    }
}

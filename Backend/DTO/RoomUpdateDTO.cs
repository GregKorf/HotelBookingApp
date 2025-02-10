using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.DTO
{
    public class RoomUpdateDTO
    {
        [Required]
        [Range(10.00, 9999.99, ErrorMessage = "Price must be between $10 and $9,999.99")]
        public decimal RoomPricePerNight { get; set; }
    }
}

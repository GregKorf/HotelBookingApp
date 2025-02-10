using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class BookingReadOnlyDTO
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
        public string? RoomNumber { get; set; }
        public string? CustomerName { get; set; } 
    }
}

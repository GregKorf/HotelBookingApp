using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.DTO
{
    public class RoomReadOnlyDTO
    {
        public int Id { get; set; }

        public decimal RoomPricePerNight { get; set; }
        public string? RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
    }
}

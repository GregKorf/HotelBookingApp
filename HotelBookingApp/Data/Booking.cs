using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.Data
{
    public class Booking : BaseEntity
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Active;
        public int CustomerId { get; set; }
        public int? AdminId { get; set; }
        public int RoomId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Admin? Admin { get; set; }
        public virtual Room Room { get; set; } = null!;
    }
}

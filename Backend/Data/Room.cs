using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.Data
{
    public class Room : BaseEntity
    {
        public int Id { get; set; }
        public decimal RoomPricePerNight { get; set; }
        public string RoomNumber { get; set; } = null!;
        public RoomType RoomType { get; set; }
        public int? AdminId { get; set; }


        public virtual Admin? Admin { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

    }
}

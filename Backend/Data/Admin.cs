namespace HotelBookingApp.Data
{
    public class Admin : BaseEntity
    {
        public int Id { get; set; }
        public string JobDescription { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int UserId { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}

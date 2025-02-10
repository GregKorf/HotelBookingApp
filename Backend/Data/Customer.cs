namespace HotelBookingApp.Data
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int UserId { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }  = new HashSet<Booking>();
    }
}

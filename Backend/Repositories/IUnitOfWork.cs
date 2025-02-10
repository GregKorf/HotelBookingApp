namespace HotelBookingApp.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        AdminRepository AdminRepository { get; }
        CustomerRepository CustomerRepository { get; }
        BookingRepository BookingRepository { get; }
        RoomRepository RoomRepository { get; }

        Task<bool> SaveAsync();
    }
}

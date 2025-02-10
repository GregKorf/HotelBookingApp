namespace HotelBookingApp.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        AdminService AdminService { get; }
        CustomerService CustomerService { get; }
        BookingService BookingService { get; }
        RoomService RoomService { get; }
    }
}

using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;

namespace HotelBookingApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, UserReadOnlyDTO>().ReverseMap();


            CreateMap<User, AdminSignUpDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => $"{src.Password}"))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.LastName}"))
                .ReverseMap();

            CreateMap<Admin, AdminSignUpDTO>()
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => $"{src.JobDescription}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.PhoneNumber}"))
                .ReverseMap();

            CreateMap<User, UserAdminReadOnlyDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.LastName}"))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => $"{src.UserRole}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Admin!.PhoneNumber}"))
                .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => $"{src.Admin!.JobDescription}"))
                .ReverseMap();


            CreateMap<User, CustomerSignUpDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => $"{src.Password}"))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.LastName}"))
                .ReverseMap();

            CreateMap<Customer, CustomerSignUpDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.City}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.PhoneNumber}"))
                .ReverseMap();

            CreateMap<User, UserCustomerReadOnlyDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => $"{src.Username}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => $"{src.Password}"))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => $"{src.FirstName}"))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => $"{src.LastName}"))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => $"{src.UserRole}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"{src.Customer!.PhoneNumber}"))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => $"{src.Customer!.City}"))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Customer!.Address}"))
                .ReverseMap();


            CreateMap<Room, RoomCreateDTO>()
                .ForMember(dest => dest.RoomPricePerNight, opt => opt.MapFrom(src => $"{src.RoomPricePerNight}"))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => $"{src.RoomNumber}"))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => $"{src.RoomType}"))
                .ReverseMap();

            CreateMap<Room, RoomReadOnlyDTO>()
                .ForMember(dest => dest.RoomPricePerNight, opt => opt.MapFrom(src => $"{src.RoomPricePerNight}"))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => $"{src.RoomNumber}"))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => $"{src.RoomType}"))
                .ReverseMap();

            CreateMap<Booking, BookingReadOnlyDTO>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.LastName))
                .ReverseMap();
        }
    }
}

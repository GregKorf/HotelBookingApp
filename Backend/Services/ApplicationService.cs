using AutoMapper;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserService UserService => new(_unitOfWork, _mapper);
        public AdminService AdminService => new(_unitOfWork, _mapper);
        public CustomerService CustomerService => new(_unitOfWork, _mapper);
        public BookingService BookingService => new(_unitOfWork, _mapper);
        public RoomService RoomService => new(_unitOfWork, _mapper);
    }
}

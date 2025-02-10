using AutoMapper;
using Azure.Core;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Encryption;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Repositories;
using Serilog;

namespace HotelBookingApp.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;


        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<RoomService>();
        }
        public async Task<RoomReadOnlyDTO> CreateRoomAsync(RoomCreateDTO roomDTO)
        {
            Room room;

            try
            {
                
                room = _mapper.Map<Room>(roomDTO);
                Room? existingRoom = await _unitOfWork.RoomRepository.GetRoomByNumberAsync(room.RoomNumber);

                if (existingRoom != null)
                {
                    throw new EntityAlreadyExistsException("Room", "Rooom with number " +
                        existingRoom.RoomNumber + " already exists");
                }

               
                await _unitOfWork.RoomRepository.AddAsync(room);

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("{Message}", "Room: " + room + " signed up successfully.");
                return _mapper.Map<RoomReadOnlyDTO>(room);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }
        

        public async Task<List<RoomReadOnlyDTO>> GetAllRoomsAsync()
        {

            List<Room> allRooms = new();
            try
            {
                allRooms = (List<Room>)await _unitOfWork.RoomRepository.GetAllAsync();
                _logger.LogInformation("{Message}", "All Rooms returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<RoomReadOnlyDTO>>(allRooms);
        }

        public async  Task<RoomReadOnlyDTO> GetRoomByIdAsync(int roomId)
        {
            Room? room = null;

            try
            {
                room = await _unitOfWork.RoomRepository.GetRoomByIdAsync(roomId);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return _mapper.Map<RoomReadOnlyDTO>(room);
        }

        public async Task<bool> UpdateRoomPriceAsync(int Id, decimal newPrice)
        {
            
            try
            {
                if (newPrice < 0)
                {
                    throw new ArgumentException("Price cannot be negative");
                }

                Room? room = await _unitOfWork.RoomRepository.GetRoomByIdAsync(Id);
                if (room == null) 
                {
                    throw new EntityNotFoundException("Room", "Rooom with id doesn't exists");
                }
                room.RoomPricePerNight = newPrice;
                await _unitOfWork.RoomRepository.UpdateAsync(room);
                await _unitOfWork.SaveAsync();
                _logger.LogInformation("{Message}", "Room: " + room.Id + " Updated successfully.");
                return true;
            }
            catch (Exception ex) 
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}

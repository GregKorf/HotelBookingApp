using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Repositories;
using Serilog;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<BookingService>();
        }
        public async Task<BookingReadOnlyDTO> CancelBookingAsync(int bookingId)
        {
            Booking? booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            try
            {
                
                if (booking == null)
                {
                    _logger.LogWarning("Booking with ID {bookingId} does not exist.", bookingId);
                    throw new EntityNotFoundException("Booking", $"Customer with ID {bookingId} does not exist.");
                }

                booking.Status = BookingStatus.Cancelled;
                await _unitOfWork.BookingRepository.UpdateAsync(booking);
                await _unitOfWork.SaveAsync();

                
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while canceling the booking: {Message}\n{StackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
            return _mapper.Map<BookingReadOnlyDTO>(booking);
        }

        public async Task<List<BookingReadOnlyDTO>> GetBookingsActiveAsync()
        {
            List<Booking> bookingsActive = new();
            try
            {
                bookingsActive = await _unitOfWork.BookingRepository.GetAllActiveBookingsAsync();  
                _logger.LogInformation("{Message}", "All Bookings returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<BookingReadOnlyDTO>>(bookingsActive);
        }

        public async Task<List<BookingReadOnlyDTO>> GetBookingsByCustomerIdAsync(int customerId)
        {
            List<Booking> bookingsByCustomer = new();
            try
            {
                Customer? customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null) 
                {
                    _logger.LogWarning("Customer with ID {customerId} does not exist.", customerId);
                    throw new EntityNotFoundException("Customer", $"Customer with ID {customerId} does not exist.");
                }

                bookingsByCustomer = await _unitOfWork.BookingRepository.GetBookingsByCustomerIdAsync(customerId);
               
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<BookingReadOnlyDTO>>(bookingsByCustomer);
        }

        public async Task<List<BookingReadOnlyDTO>> GetBookingsByRoomIdAsync(int roomId)
        {
            List<Booking> bookingsByRoom = new();
            try
            {
                Room? room = await _unitOfWork.RoomRepository.GetByIdAsync(roomId);
                if (room == null)
                {
                    _logger.LogWarning("Room with ID {roomId} does not exist.", roomId);
                    throw new EntityNotFoundException("Room", $"Room with ID {roomId} does not exist.");
                }

                bookingsByRoom = await _unitOfWork.BookingRepository.GetBookingsByCustomerIdAsync(roomId);

            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<BookingReadOnlyDTO>>(bookingsByRoom);
        }


    }
}

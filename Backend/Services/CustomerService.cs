using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Encryption;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Repositories;
using Serilog;

namespace HotelBookingApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<CustomerService>();
        }

        public async Task<BookingReadOnlyDTO> BookRoomAsync(BookingCreateDTO bookingRequest, int customerId)
        {

            try
            {
                Customer? customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} does not exist.", customerId);
                    throw new EntityNotFoundException("Customer", $"Customer with ID {customerId} does not exist.");
                }

                Room? room = await _unitOfWork.RoomRepository.GetByIdAsync(bookingRequest.RoomId);
                if (room == null)
                {
                    _logger.LogWarning("Room with ID {bookingRequest.RoomId} does not exist.", bookingRequest.RoomId);
                    throw new EntityNotFoundException("Room", $"Room with ID {bookingRequest.RoomId} does not exist.");
                }

                bool roomAvailable = await _unitOfWork.BookingRepository.IsRoomAvailableAsync(bookingRequest.RoomId, bookingRequest.CheckInDate, bookingRequest.CheckOutDate);
                if (!roomAvailable)
                {
                    _logger.LogWarning("Room with ID {bookingRequest.RoomId} isnt available.", bookingRequest.RoomId);
                    throw new EntityNotFoundException("Room", $"Room with ID {bookingRequest.RoomId} isnt available.");
                }


                Booking booking = new Booking
                {
                    CheckInDate = bookingRequest.CheckInDate,
                    CheckOutDate = bookingRequest.CheckOutDate,
                    Status = BookingStatus.Active,
                    CustomerId = customerId,
                    RoomId = bookingRequest.RoomId
                };

                await _unitOfWork.BookingRepository.AddAsync(booking);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<BookingReadOnlyDTO>(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<List<UserCustomerReadOnlyDTO>> GetAllCustomersAsync()
        {
            List<User> usersCustomers = new();
            try
            {
                usersCustomers = await _unitOfWork.CustomerRepository.GetAllUsersCustomersAsync();
                _logger.LogInformation("{Message}", "All Customers returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<UserCustomerReadOnlyDTO>>(usersCustomers);
        }

        public async Task<List<BookingReadOnlyDTO>> GetCustomerBookingsAsync(int id)
        {
            List<Booking> bookingsCustomers = new();
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} does not exist.", id);
                    throw new EntityNotFoundException("Customer",$"Customer with ID {id} does not exist.");
                }

                bookingsCustomers = await _unitOfWork.BookingRepository.GetBookingsByCustomerIdAsync(id);
                _logger.LogInformation("{Message}", "All Bookings for the customer has been returned");
            }
            catch (Exception ex) 
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<BookingReadOnlyDTO>>(bookingsCustomers);
        }

        public async Task<UserCustomerReadOnlyDTO?> GetCustomerByUsernameAsync(string username)
        {
            User? user = await _unitOfWork.CustomerRepository.GetUserCustomerByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserCustomerReadOnlyDTO>(user);
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await _unitOfWork.CustomerRepository.GetCustomerByUserIdAsync(userId);
        }
        public async Task<UserReadOnlyDTO> SignUpCustomerAsync(CustomerSignUpDTO request)
            {
                Customer customer;
                User user;

                try
                {
                    
                    user = _mapper.Map<User>(request);
                    User? existingUser = await _unitOfWork.UserRepository.GetUserByUsername(user.Username);
                   
                    if (existingUser != null)
                    {
                        throw new EntityAlreadyExistsException("User", "User with username " +
                            existingUser.Username + " already exists");
                    }

                    // Check for existing phone number
                    customer = _mapper.Map<Customer>(request);
                    Customer? existingPhoneNumber = await _unitOfWork.CustomerRepository.GetCustomerByPhoneNumberAsync(customer.PhoneNumber);
                    if (existingPhoneNumber != null)
                    {
                        throw new EntityAlreadyExistsException("User", $"User with phone number {existingPhoneNumber.PhoneNumber} already exists");
                    }
                    user.UserRole = UserRole.Customer;

                    user.Password = EncryptionUtil.EncryptPassword(user.Password);
                    
                    await _unitOfWork.UserRepository.AddAsync(user);


                    await _unitOfWork.CustomerRepository.AddAsync(customer);
                    user.Customer = customer;


                    await _unitOfWork.SaveAsync();
                    _logger.LogInformation("{Message}", "Customer: " + customer + " signed up successfully.");
                    return _mapper.Map<UserReadOnlyDTO>(user); ;
                }
                catch (Exception ex)
                {
                    _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                    throw;
                }
             }
        }
 }

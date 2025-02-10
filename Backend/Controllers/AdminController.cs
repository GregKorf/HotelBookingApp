using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.DTO;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelBookingApp.Controllers
{ 
    public class AdminController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AdminController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserReadOnlyDTO>> SignUpAdmin(AdminSignUpDTO request)
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState
                .Where(e => e.Value!.Errors.Any())
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }

            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }

            UserReadOnlyDTO result = await _applicationService.AdminService.SignUpAdminAsync(request);
            return CreatedAtAction(nameof(SignUpAdmin), result);

        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<JwtTokenDTO>> LoginUserAsync(UserLoginDTO credentials)
        {
            var user = await _applicationService.UserService.VerifyAndGetUserAsync(credentials);
            if (user == null)
            {
                throw new EntityNotAuthorizedException("User", "BadCredentials");
            }
            if (user.UserRole != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Access Denied: Only admins can log in here.");
            }
            var userToken = _applicationService.UserService.CreateUserToken(user.Id, user.Username!, user.Email!,
                user.UserRole, _configuration["Authentication:SecretKey"]!);

            JwtTokenDTO token = new()
            {
                Token = userToken
            };

            return Ok(token);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BookingReadOnlyDTO>>> GetAllBookings()
        {
            var bookings = await _applicationService.BookingService.GetBookingsActiveAsync();
            return Ok(bookings);
        }

        [HttpPut("{bookingId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId,BookingCreateDTO updateBooking)
        {

            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            await _applicationService.BookingService.CancelBookingAsync(bookingId);
            return Ok();
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoomReadOnlyDTO>> CreateRoom(RoomCreateDTO request)
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState
                .Where(e => e.Value!.Errors.Any())
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value!.Errors.Select(error => error.ErrorMessage).ToArray()
                });
                throw new InvalidRegistrationException("ErrorsInRegistation: " + errors);
            }

            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }

            RoomReadOnlyDTO result = await _applicationService.RoomService.CreateRoomAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<RoomReadOnlyDTO>>> GetAllRooms()
        {

            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            var rooms = await _applicationService.RoomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpPut("{roomId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoomPrice(int roomId, decimal price)
        {
            if (price < 0) 
            {
                throw new InvalidArgumentException("InvalidArgument", "Price cannot be negative");
            }
            RoomReadOnlyDTO roomExists = await _applicationService.RoomService.GetRoomByIdAsync(roomId);
            if (roomExists == null)
            {
                throw new EntityNotFoundException("NotFound", "Room not found");
            }
            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }
            await _applicationService.RoomService.UpdateRoomPriceAsync(roomId, price);
            return Ok();
        }

        private async Task<ActionResult<UserReadOnlyDTO>> GetUserById(int id)
        {
            var user = await _applicationService.UserService.GetUserByIdAsync(id) ?? throw new EntityNotFoundException("User", "User: " + id + " NotFound");
            var returnedDto = _mapper.Map<UserReadOnlyDTO>(user);
            return Ok(returnedDto);
        }
    }

}


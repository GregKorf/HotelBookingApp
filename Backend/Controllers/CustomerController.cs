using System.Security.Claims;
using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.DTO;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers
{

    public class CustomerController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CustomerController(IApplicationService applicationService, IConfiguration configuration,
            IMapper mapper)
            : base(applicationService)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserReadOnlyDTO>> SignUpCustomer(CustomerSignUpDTO request)
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

            UserReadOnlyDTO result = await _applicationService.CustomerService.SignUpCustomerAsync(request);
            return CreatedAtAction(nameof(SignUpCustomer), result);

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
            if (user.UserRole != UserRole.Customer)
            {
                throw new UnauthorizedAccessException("Access Denied: Only customers can log in here.");
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
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<List<BookingReadOnlyDTO>>> GetCustomerBookings()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user identity." });
            }
            var customer = await _applicationService.CustomerService.GetCustomerByUserIdAsync(userId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer profile not found." });
            }
            var customerId = customer.Id;
            List<BookingReadOnlyDTO> bookings = await _applicationService.CustomerService.GetCustomerBookingsAsync(customerId);
            if (bookings == null || !bookings.Any())
            {
                return NotFound(new { message = "No bookings found for the specified customer." });
            }

            if (_applicationService == null)
            {
                throw new ServerException("ApplicationServiceNull", "Application Service is null");
            }

            return Ok(bookings);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<BookingReadOnlyDTO>> BookRoom(BookingCreateDTO request)
        {
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { message = "Invalid user identity." });
                }

                var customer = await _applicationService.CustomerService.GetCustomerByUserIdAsync(userId);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer profile not found." });
                }

                var customerId = customer.Id;
                BookingReadOnlyDTO booking = await _applicationService.CustomerService.BookRoomAsync(request, customerId);
                return CreatedAtAction(nameof(GetCustomerBookings), new { customerId = userId }, booking);
            }
        }

        //[HttpGet("rooms")]
        //public async Task<IActionResult> GetAvailableRooms(DateTime checkInDate,  DateTime checkOutDate)
        //{
        //    if (checkInDate >= checkOutDate)
        //        return BadRequest("Check-in date must be before check-out date.");

        //    var rooms = await _applicationService.BookingService.(checkInDate, checkOutDate);
        //    return Ok(rooms);
        //}

    }
}


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using HotelBookingApp.Core.Enums;
using HotelBookingApp.Core.Filters;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Repositories;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace HotelBookingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;


        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<UserService>();
        }
        public Task<List<User>> GetAllUsersFiltered(int pageNumber, int pageSize, UserFiltersDTO userFiltersDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            User? user = null;

            try
            {
                user = await _unitOfWork.UserRepository.GetUserByUsername(username);
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}{Excpetion}", e.Message, e.StackTrace);
            }
            return user;
        }

        public async Task<User?> VerifyAndGetUserAsync(UserLoginDTO credentials)
        {
            User? user;

            try
            {
                user = await _unitOfWork.UserRepository.GetUserAsync(credentials.Username!, credentials.Password!);
                _logger.LogInformation("{Message}", "User: " + user + " found and returned.");   // ToDo toString()
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
            return user;
        }
        public string CreateUserToken(int userId, string username, string email, UserRole? userRole,
            string appSecurityKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsInfo = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, userRole.ToString()!)
            };

            var jwtSecurityToken = new JwtSecurityToken("https://localhost:5002", "https://localhost:4200", claimsInfo, DateTime.UtcNow,
                DateTime.UtcNow.AddHours(3), signingCredentials);
            //var jwtSecurityToken = new JwtSecurityToken(null, null, claimsInfo, DateTime.UtcNow,
            //    DateTime.UtcNow.AddHours(3), signingCredentials);

            // Serialize the token
            var userToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return userToken;
        }
    }
}

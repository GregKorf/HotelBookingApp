using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AutoMapper;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Encryption;
using HotelBookingApp.Repositories;
using HotelBookingApp.Exceptions;
using Serilog;
using HotelBookingApp.Core.Enums;

namespace HotelBookingApp.Services
{
    public class AdminService : IAdminService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;


        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = new LoggerFactory().AddSerilog().CreateLogger<AdminService>();
        }

        public async Task<UserAdminReadOnlyDTO?> GetAdminByUsernameAsync(string username)
        {
            User? user = await _unitOfWork.AdminRepository.GetUserAdminByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserAdminReadOnlyDTO>(user);
        }

        public async Task<List<UserAdminReadOnlyDTO>> GetAllUsersAdminsAsync()
        {
            List<User> usersAdmins = new();
            try
            {
                usersAdmins = await _unitOfWork.AdminRepository.GetAllUsersAdminsAsync();
                _logger.LogInformation("{Message}", "All Admins returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
            }
            return _mapper.Map<List<UserAdminReadOnlyDTO>>(usersAdmins);
        }

        public async Task<UserReadOnlyDTO> SignUpAdminAsync(AdminSignUpDTO request)
        {
            Admin admin;
            User user;

            try
            {
                //user = ExtractUser(request);
                user = _mapper.Map<User>(request);
                User? existingUser = await _unitOfWork.UserRepository.GetUserByUsername(user.Username);

                if (existingUser != null)
                {
                    throw new EntityAlreadyExistsException("User", "User with username " +
                        existingUser.Username + " already exists");
                }

                // Check for existing phone number
                admin = _mapper.Map<Admin>(request);
                Admin? existingPhoneNumber = await _unitOfWork.AdminRepository.GetAdminByPhoneNumberAsync(admin.PhoneNumber); 
                if (existingPhoneNumber != null)
                { 
                    throw new EntityAlreadyExistsException("User", $"User with phone number {existingPhoneNumber.PhoneNumber} already exists"); 
                }

                user.UserRole = UserRole.Admin;

                user.Password = EncryptionUtil.EncryptPassword(user.Password);

                await _unitOfWork.UserRepository.AddAsync(user);


                await _unitOfWork.AdminRepository.AddAsync(admin);
                user.Admin = admin;
                

                await _unitOfWork.SaveAsync();
                _logger.LogInformation("{Message}", "Admin: " + admin + " signed up successfully.");
                return _mapper.Map<UserReadOnlyDTO>(user); 
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}{Exception}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}

using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Application.Responses;
using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserResponseDto>> RegisterUserAsync(
            RegisterUserREquestDto request)

        {
            var user = new User
            {
                FirstName = request.FirstName,

                LastName = request.LastName,

                Email = request.Email,

                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),

                Role = "User",

                IsActive = true,

                CreatedDate = DateTime.Now,
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            var response = new UserResponseDto
            {
                UserId = createdUser.UserId,

                FullName = $"{createdUser.FirstName} {createdUser.LastName}",

                Email = createdUser.Email,

                Role = createdUser.Role
            };

            return new ApiResponse<UserResponseDto>
            {
                Success = true,

                Message = "User registered successfully",

                Data = response
            };
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(
            LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,

                    Message = "Invalid email or password"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash))
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,

                    Message = "Invalid email or password"
                };
            }

            var response = new LoginResponseDto
            {
                UserId = user.UserId,

                FullName = $"{user.FirstName} {user.LastName}",

                Email = user.Email,

                Role = user.Role
            };

            return new ApiResponse<LoginResponseDto>
            {
                Success = true,

                Message = "Login Successful",

                Data = response
            };
        }
    }
}
using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Responses;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserResponseDto>> RegisterUserAsync(
            RegisterUserREquestDto requst);

        Task<ApiResponse<LoginResponseDto>> LoginAsync(
            LoginRequestDto request);
    }
}
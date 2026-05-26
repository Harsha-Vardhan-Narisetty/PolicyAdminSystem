using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Responses;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderService
    {
        Task<ApiResponse<IEnumerable<PolicyHolderResponseDto>>> GetAllPolicyHoldersAsync();

        Task<ApiResponse<PolicyHolderResponseDto>> CreatePolicyHolderAsync(CreatePolicyHolderRequestDto request);
    }
}
using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Responses;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderService
    {
        Task<ApiResponse<IEnumerable<PolicyHolderResponseDto>>> GetAllPolicyHoldersAsync();

        Task<ApiResponse<PolicyHolderResponseDto>> CreatePolicyHolderAsync(
            CreatePolicyHolderRequestDto request);

        Task<ApiResponse<PolicyHolderResponseDto>> UpdatePolicyHolderAsync(
            int id,
            UpdatePolicyHolderRequestDto request);

        Task<ApiResponse<string>> DeletePolicyHolderAsync(int id);
    }
}
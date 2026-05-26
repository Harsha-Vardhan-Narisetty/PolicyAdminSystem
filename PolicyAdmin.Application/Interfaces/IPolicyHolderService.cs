using PolicyAdmin.Application.DTOs;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderService
    {
        Task<IEnumerable<PolicyHolderResponseDto>> GetAllPolicyHoldersAsync();

        Task<PolicyHolderResponseDto> CreatePolicyHolderAsync(CreatePolicyHolderRequestDto request);
    }
}
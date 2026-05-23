using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderService
    {
        Task<IEnumerable<PolicyHolder>> GetAllPolicyHoldersAsync();
    }
}
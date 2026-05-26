using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderRepository
    {
        Task<IEnumerable<PolicyHolder>> GetAllAsync();

        Task<PolicyHolder> AddAsync(PolicyHolder policyHolder);
    }
}
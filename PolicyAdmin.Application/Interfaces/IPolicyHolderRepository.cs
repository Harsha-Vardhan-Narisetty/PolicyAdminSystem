using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderRepository
    {
        Task<IEnumerable<PolicyHolder>> GetAllAsync();

        Task<PolicyHolder> AddAsync(PolicyHolder policyHolder);

        Task<PolicyHolder?> GetByIdAsync(int id);

        Task<PolicyHolder> UpdateAsync(PolicyHolder policyHolder);
    }
}
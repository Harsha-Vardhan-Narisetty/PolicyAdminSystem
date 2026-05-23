using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Interfaces
{
    public interface IPolicyHolderRepository
    {
        Task<IEnumerable<PolicyHolder>> GetAllAsync();
    }
}
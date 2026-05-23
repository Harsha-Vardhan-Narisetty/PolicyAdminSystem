using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Services
{
    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public PolicyHolderService(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllPolicyHoldersAsync()
        {
            return await _policyHolderRepository.GetAllAsync();
        }
    }
}
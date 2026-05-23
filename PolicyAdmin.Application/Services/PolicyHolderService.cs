using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Application.DTOs;
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

        public async Task<IEnumerable<PolicyHolderResponseDto>> GetAllPolicyHoldersAsync()
        {
            var policyHolders = await _policyHolderRepository.GetAllAsync();

            var response = policyHolders.Select(PolicyHolder => new PolicyHolderResponseDto
            {
                PolicyHolderId = PolicyHolder.PolicyHolderId,

                FullName = $"{PolicyHolder.FirstName} {PolicyHolder.LastName}",

                Email = PolicyHolder.Email,

                PhoneNumber = PolicyHolder.PhoneNumber,

                City = PolicyHolder.City
            });

            return response;
        }
    }
}
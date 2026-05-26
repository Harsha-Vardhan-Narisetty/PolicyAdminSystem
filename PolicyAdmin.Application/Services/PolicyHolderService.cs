using PolicyAdmin.Application.DTOs;
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

        public async Task<IEnumerable<PolicyHolderResponseDto>> GetAllPolicyHoldersAsync()
        {

            var policyHolders = await _policyHolderRepository.GetAllAsync();

            var response = policyHolders.Select(policyHolder => new PolicyHolderResponseDto
            {
                PolicyHolderId = policyHolder.PolicyHolderId,

                FullName = $"{policyHolder.FirstName} {policyHolder.LastName}",

                Email = policyHolder.Email,

                PhoneNumber = policyHolder.PhoneNumber,

                City = policyHolder.City
            });

            return response;
        }

        public async Task<PolicyHolderResponseDto> CreatePolicyHolderAsync(CreatePolicyHolderRequestDto request)
        {
            var policyHolder = new PolicyHolder
            {
                FirstName = request.FirstName,

                LastName = request.LastName,

                DateOfBirth = request.DateOfBirth,

                Gender = request.Gender,

                Email = request.Email,

                PhoneNumber = request.PhoneNumber,

                AddressLine1 = request.AddressLine1,

                AddressLine2 = request.AddressLine2,

                City = request.City,

                State = request.State,

                PostalCode = request.PostalCode,

                Country = request.Country,

                CreatedDate = DateTime.Now,

                IsActive = true
            };

            var createdPolicyHolder = await _policyHolderRepository.AddAsync(policyHolder);

            return new PolicyHolderResponseDto
            {
                PolicyHolderId = createdPolicyHolder.PolicyHolderId,

                FullName = $"{createdPolicyHolder.FirstName} {createdPolicyHolder.LastName}",

                Email = createdPolicyHolder.Email,

                PhoneNumber = createdPolicyHolder.PhoneNumber,

                City = createdPolicyHolder.City
            };
        }
    }
}
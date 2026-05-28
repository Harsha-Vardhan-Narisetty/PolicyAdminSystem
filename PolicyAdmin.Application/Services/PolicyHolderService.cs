using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Application.Responses;
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

        public async Task<ApiResponse<IEnumerable<PolicyHolderResponseDto>>> GetAllPolicyHoldersAsync()
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

            return new ApiResponse<IEnumerable<PolicyHolderResponseDto>>
            {
                Success = true,

                Message = "Policy holders fetched successfully",

                Data = response
            };
        }

        public async Task<ApiResponse<PolicyHolderResponseDto>> CreatePolicyHolderAsync(CreatePolicyHolderRequestDto request)
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

            var responseDto = new PolicyHolderResponseDto
            {
                PolicyHolderId = createdPolicyHolder.PolicyHolderId,

                FullName = $"{createdPolicyHolder.FirstName} {createdPolicyHolder.LastName}",

                Email = createdPolicyHolder.Email,

                PhoneNumber = createdPolicyHolder.PhoneNumber,

                City = createdPolicyHolder.City
            };

            return new ApiResponse<PolicyHolderResponseDto>
            {
                Success = true,

                Message = "Policy holder created successfully",

                Data = responseDto
            };
        }

        public async Task<ApiResponse<PolicyHolderResponseDto>> UpdatePolicyHolderAsync(
            int id,
            UpdatePolicyHolderRequestDto request)
        {
            var existingPolicyHolder = await _policyHolderRepository.GetByIdAsync(id);

            if (existingPolicyHolder == null)
            {
                return new ApiResponse<PolicyHolderResponseDto>
                {
                    Success = false,

                    Message = $"Policy holder with ID {id} not found"
                };
            }

            existingPolicyHolder.FirstName = request.FirstName;

            existingPolicyHolder.LastName = request.LastName;

            existingPolicyHolder.DateOfBirth = request.DateOfBirth;

            existingPolicyHolder.Gender = request.Gender;

            existingPolicyHolder.Email = request.Email;

            existingPolicyHolder.PhoneNumber = request.PhoneNumber;

            existingPolicyHolder.AddressLine1 = request.AddressLine1;

            existingPolicyHolder.AddressLine2 = request.AddressLine2;

            existingPolicyHolder.City = request.City;

            existingPolicyHolder.State = request.State;

            existingPolicyHolder.PostalCode = request.PostalCode;

            existingPolicyHolder.Country = request.Country;

            var updatedPolicyHolder = await _policyHolderRepository.UpdateAsync(existingPolicyHolder);

            var responseDto = new PolicyHolderResponseDto
            {
                PolicyHolderId = updatedPolicyHolder.PolicyHolderId,

                FullName = $"{updatedPolicyHolder.FirstName} {updatedPolicyHolder.LastName}",

                Email = updatedPolicyHolder.Email,

                PhoneNumber = updatedPolicyHolder.PhoneNumber,

                City = updatedPolicyHolder.City
            };

            return new ApiResponse<PolicyHolderResponseDto>
            {
                Success = true,

                Message = "Policy holder updated successfully",

                Data = responseDto
            };

        }

        public async Task<ApiResponse<string>> DeletePolicyHolderAsync(int id)
        {
            var existingPolicyHolder = await _policyHolderRepository.GetByIdAsync(id);

            if(existingPolicyHolder == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,

                    Message = $"Policy holder with ID {id} not found"
                };
            }

            await _policyHolderRepository.SoftDeleteAsync(existingPolicyHolder);

            return new ApiResponse<string>
            {
                Success = true,

                Message = "Policy holder deleted successfully",

                Data = $"Deleted policy holder ID: {id}"
            };
        }
    }
}
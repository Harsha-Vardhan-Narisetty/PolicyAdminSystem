using Microsoft.AspNetCore.Mvc;
using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Interfaces;

namespace PolicyAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHoldersController : ControllerBase
    {
        private readonly IPolicyHolderService _policyHolderService;

        public PolicyHoldersController(IPolicyHolderService policyHolderService)
        {
            _policyHolderService = policyHolderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicyHolders()
        {
            var policyHolders = await _policyHolderService.GetAllPolicyHoldersAsync();

            return Ok(policyHolders);
        }

        [HttpPost]
        public async Task<IActionResult> createPolicyHolder(CreatePolicyHolderRequestDto request)
        {
            var createdPolicyHolder = await _policyHolderService.CreatePolicyHolderAsync(request);

            return Ok(createdPolicyHolder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolicyHolder(
            int id,
            UpdatePolicyHolderRequestDto request)
        {
            var response = await _policyHolderService.UpdatePolicyHolderAsync(id, request);

            if(!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
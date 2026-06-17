using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyAdmin.Application.DTOs;
using PolicyAdmin.Application.Interfaces;

namespace PolicyAdmin.API.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> createPolicyHolder(CreatePolicyHolderRequestDto request)
        {
            var createdPolicyHolder = await _policyHolderService.CreatePolicyHolderAsync(request);

            return Ok(createdPolicyHolder);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePolicyHolder(int id)
        {
            var response = await _policyHolderService.DeletePolicyHolderAsync(id);

            if(!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);

        }

        [Authorize]
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var email = User.FindFirst(
                System.Security.Claims.ClaimTypes.Email)?.Value;

            var role = User.FindFirst(
                System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
                UserId = userId,
                Email = email,
                Role = role
            });
        }
    }
}
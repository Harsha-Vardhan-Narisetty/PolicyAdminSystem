using Microsoft.AspNetCore.Mvc;
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
    }
}
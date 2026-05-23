using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PolicyAdmin.Persistence.Contexts;
using PolicyAdmin.Application.Interfaces;

namespace PolicyAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHoldersController : ControllerBase
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public PolicyHoldersController(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicyHolders()
        {
            var policyHolders = await _policyHolderRepository.GetAllAsync();

            return Ok(policyHolders);
        }
    }
}
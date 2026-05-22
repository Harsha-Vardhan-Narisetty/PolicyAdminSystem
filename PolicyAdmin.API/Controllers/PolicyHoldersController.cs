using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyAdmin.Persistence.Contexts;

namespace PolicyAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHoldersController : ControllerBase
    {
        private readonly PolicyAdminDbContext _context;

        public PolicyHoldersController(PolicyAdminDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPolicyHolders()
        {
            var policyHolders = await _context.PolicyHolders.ToListAsync();

            return Ok(policyHolders);
        }
    }
}
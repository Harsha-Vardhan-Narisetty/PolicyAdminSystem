using Microsoft.EntityFrameworkCore;
using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Persistence.Contexts;
using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Persistence.Repositories
{
    public class PolicyHolderRepository : IPolicyHolderRepository
    {
        private readonly PolicyAdminDbContext _context;

        public PolicyHolderRepository(PolicyAdminDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllAsync()
        {
            return await _context.PolicyHolders.ToListAsync();
        }
    }
}

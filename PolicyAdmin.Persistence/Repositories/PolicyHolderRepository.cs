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
            return await _context.PolicyHolders
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<PolicyHolder> AddAsync(PolicyHolder policyHolder)
        {
            await _context.PolicyHolders.AddAsync(policyHolder);

            await _context.SaveChangesAsync();

            return policyHolder;
        }

        public async Task<PolicyHolder?> GetByIdAsync(int id)
        {
            return await _context.PolicyHolders
                .FirstOrDefaultAsync(x =>
                    x.PolicyHolderId == id &&
                    x.IsActive);
        }

        public async Task<PolicyHolder> UpdateAsync(PolicyHolder policyHolder)
        {
            _context.PolicyHolders.Update(policyHolder);

            await _context.SaveChangesAsync();

            return policyHolder;
        }

        public async Task SoftDeleteAsync(PolicyHolder policyHolder)
        {
            policyHolder.IsActive = false;

            _context.PolicyHolders.Update(policyHolder);

            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PolicyAdmin.Application.Interfaces;
using PolicyAdmin.Domain.Entities;
using PolicyAdmin.Persistence.Contexts;

namespace PolicyAdmin.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PolicyAdminDbContext _context;

        public UserRepository(PolicyAdminDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
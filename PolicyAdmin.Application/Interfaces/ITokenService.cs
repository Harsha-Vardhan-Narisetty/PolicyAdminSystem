using PolicyAdmin.Domain.Entities;

namespace PolicyAdmin.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
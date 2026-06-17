using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PolicyAdmin.Application.Interfaces;

namespace PolicyAdmin.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var userId =
                    _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst(ClaimTypes.NameIdentifier)?
                    .Value;

                if (int.TryParse(userId, out var id))
                {
                    return id;
                }

                return null;
            }
        }
    }
}
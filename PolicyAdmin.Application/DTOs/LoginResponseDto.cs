namespace PolicyAdmin.Application.DTOs
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Token { get; set; } = string.Empty!;

        public DateTime ExpiresAt { get; set; }
    }
}
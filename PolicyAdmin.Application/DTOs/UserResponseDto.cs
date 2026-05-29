namespace PolicyAdmin.Application.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get ; set; } = null!;
    }
}
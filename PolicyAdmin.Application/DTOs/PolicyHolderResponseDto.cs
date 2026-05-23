namespace PolicyAdmin.Application.DTOs
{
    public class PolicyHolderResponseDto
    {
        public int PolicyHolderId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}

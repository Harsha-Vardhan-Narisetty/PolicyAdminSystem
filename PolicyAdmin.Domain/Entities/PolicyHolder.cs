using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolicyAdmin.Domain.Entities;

public partial class PolicyHolder
{
    [Key]
    public int PolicyHolderId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    [StringLength(20)]
    public string Gender { get; set; } = null!;

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(200)]
    public string AddressLine1 { get; set; } = null!;

    [StringLength(200)]
    public string? AddressLine2 { get; set; }

    [StringLength(100)]
    public string City { get; set; } = null!;

    [StringLength(100)]
    public string State { get; set; } = null!;

    [StringLength(20)]
    public string PostalCode { get; set; } = null!;

    [StringLength(100)]
    public string Country { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }
}

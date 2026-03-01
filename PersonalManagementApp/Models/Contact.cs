using System.ComponentModel.DataAnnotations;

namespace PersonalManagementApp.Models;

public class Contact
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(120)]
    public string? Email { get; set; }

    [Phone]
    [StringLength(40)]
    public string? PhoneNumber { get; set; }

    [StringLength(120)]
    public string? Company { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

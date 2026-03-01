using System.ComponentModel.DataAnnotations;

namespace PersonalManagementApp.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    [Required]
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;

    public bool IsCompleted { get; set; }

    [StringLength(80)]
    public string Category { get; set; } = "General";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}

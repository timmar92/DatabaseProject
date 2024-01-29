using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

[Index(nameof(TaskName), IsUnique = true)]
public class TaskEntity
{
    [Key]
    public int TaskId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string TaskName { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Required]
    [ForeignKey(nameof(CategoryEntity))]
    public int CategoryId { get; set; }

    [Required]
    [ForeignKey(nameof(StatusEntity))]
    public int StatusId { get; set; }

    public virtual CategoryEntity Category { get; set; } = null!;
    public virtual StatusEntity Status { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AssignmentEntity
{
    [Key]
    public int AssignmentId { get; set; }

    [Required]
    [ForeignKey(nameof(UserEntity))]
    public int UserId { get; set; }

    [Required]
    [ForeignKey(nameof(TaskEntity))]
    public int TaskId { get; set;}

    public virtual UserEntity User { get; set; } = null!;
    public virtual TaskEntity Task { get; set; } = null!;
}

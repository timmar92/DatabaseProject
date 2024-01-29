using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;


[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Email { get; set; } = null!;

    public virtual ICollection<AssignmentEntity> Assignments { get; set; } = new List<AssignmentEntity>();
}

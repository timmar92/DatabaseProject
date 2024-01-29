using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class StatusEntity
{
    [Key]
    public int StatusId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string StatusName { get; set; } = null!;

    public virtual ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}

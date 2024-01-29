using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CategoryEntity
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string CategoryName { get; set; } = null!;

    public virtual ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}

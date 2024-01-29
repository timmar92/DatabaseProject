using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<AssignmentEntity> Assignments { get; set; } = null!;
        public DbSet<CategoryEntity> Categories { get; set; } = null!;
        public DbSet<StatusEntity> Statuses { get; set; } = null!;
    }
}
